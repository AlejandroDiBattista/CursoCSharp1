using System;
using System.Collections.Generic;
using System.Linq;

namespace PruebaHashCode {

    static public class Verify {

        public static void NotEmpty(this string a, string mensaje = "") {
            if(string.IsNullOrEmpty(a)) {
                throw new ArgumentNullException(mensaje);
            }
        }

        public static void NotNull(this object a, string mensaje = "") {
            if(a == null) {
                throw new ArgumentNullException(mensaje);
            }
        }

        static public bool Equals(this string a, string b) => String.Compare(a, b, true) == 0;

        public static bool Contains(this HashSet<Persona> personas, Persona persona) {
            if(personas == null) return false;
            if(persona == null) return false;
            return personas.Any(p => p.Equals(persona));
        }
    }

    public class Persona : IEquatable<Persona> {
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public Persona(string nombre, string apellido) {
            nombre.NotEmpty();
            Verify.NotNull(apellido);

            Nombre   = nombre;
            Apellido = apellido;
        }

        public bool Equals(Persona other) {
            if(other == null) return false;
            return Verify.Equals(Nombre, other.Nombre) && Verify.Equals(Apellido, other.Apellido);
        }
        public override bool Equals(object other) => other is Persona persona && this.Equals(persona);

        public static bool operator ==(Persona a, Persona b) =>   a?.Equals(b) ?? false;
        public static bool operator !=(Persona a, Persona b) => !(a?.Equals(b) ?? false);

        public override int GetHashCode() => (Nombre, Apellido).GetHashCode();
    }

    static class Program {
        static void Main(string[] args) {
            var a = new Persona("Alejandro", "Di Battista");
            var b = new Persona("ALEJANDRO", "DI BATTISTA");
            var c = new Persona("Alejandro", "DI BATTISTA");

            var d = new HashSet<Persona>();
            d.Add(a);
            d.Add(b);

            Persona x = null;

            Console.WriteLine("DEMO GetHashCode");
            Console.WriteLine($"A:GHC {a.GetHashCode()}") ;
            Console.WriteLine($"B:GHC {b.GetHashCode()}");
            Console.WriteLine($"A==B {a.Equals(b)}");
            Console.WriteLine($"A==B {a == b}");

            Console.WriteLine("Original >> ");
            Console.WriteLine($"A in D {d.Contains(a)}");
            Console.WriteLine($"B in D {d.Contains(b)}");
            Console.WriteLine($"C in D {d.Contains(c)}");
            Console.WriteLine($"C in D {x == c}");
            Console.WriteLine("Con Linq>> ");
            Console.WriteLine($"A in D {d.Contains(a)}");
            Console.WriteLine($"B in D {d.Contains(b)}");
            Console.WriteLine($"C in D {d.Contains(c)}");
            Console.ReadLine();
        }
    }
}
