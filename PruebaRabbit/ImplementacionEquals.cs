using System;
using System.Dynamic;
using System.Text.RegularExpressions;

namespace Prueba1
{
    class Program
    {
        public class Punto :IEquatable<Punto>
        {
            public int X { get; set; }
            public int Y { get; set; }

            public override bool Equals(object obj) => Equals(obj as Punto);
            public bool Equals(Punto other) => !ReferenceEquals(other, null) && (X, Y) == (other.X, other.Y);
            public override int GetHashCode() => (X, Y).GetHashCode();

            static public bool operator ==(Punto a, Punto b) =>  a?.Equals(b) ?? false;
            static public bool operator !=(Punto a, Punto b) => !a?.Equals(b) ?? true;
        }

        public static dynamic Copia { get; set; } = new ExpandoObject();

        public static void Copiar(Punto p)
        {
            Copia = p;
        }

        static void PruebaCopiarEnExpandoObject()
        {
            var p = new Punto { X = 10, Y = 20 };
            Copiar(p);
            Console.WriteLine(Copia.ToString());
            Console.WriteLine($"Copia > X:{Copia.X} Y:{Copia.Y}");
            Console.WriteLine($"P     > X:{p.X} Y:{p.Y}");
            Copia.X = 100;
            Console.WriteLine($"Copia > X:{Copia.X} Y:{Copia.Y}");
            Console.WriteLine($"P     > X:{p.X} Y:{p.Y}");
            p = new Punto { X = 1000, Y = 1000 };
            Console.WriteLine($"Copia > X:{Copia.X} Y:{Copia.Y}");
            Console.WriteLine($"P     > X:{p.X} Y:{p.Y}");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            var a = new Punto{ X = 10, Y = 10};
            object b = new Punto { X = 10, Y = 10 };
            var c = new Punto { X = 20, Y = 20 };

            Console.WriteLine(" » Prueba Implementación Equals");
            Console.WriteLine($" · A eq B     > {a.Equals(b)}");
            Console.WriteLine($" · A eq C     > {a.Equals(c)}");
            Console.WriteLine($" · A eq ERROR > {a.Equals("Hola")}");
            Console.WriteLine($" · A eq NULL  > {a.Equals(null)}");
            Console.WriteLine($" · A == NULL  > {a == null}");
            Console.WriteLine($" · (A) == (A) > {(a) == (a)}");
            Console.WriteLine($" · (A) == (B) > {(a) == (b)}");
            Console.WriteLine($" · (A) eq (B) > {(a).Equals(b)}");
            Console.ReadLine();

        }
    }
}
