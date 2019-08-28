using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaRabbit {
    interface IPersona {
        string Nombre { get; set; }
        int Edad { get; set; }
    }

    class Persona : IPersona {
        public string Nombre { get; set; }
        public int Edad { get; set; }

        public Persona(string nombre, int edad) {
            Nombre = nombre;
            Edad = edad;
        }

        public override string ToString() => $"Nombr: {Nombre}, Edad: {Edad}";
    }

    class Programa { 
        static void Main(string[] args) {
            Console.WriteLine(" > Prueba Instancia Interface");
            var a = new IPersona[] { new Persona("Ale", 52) };
            foreach(var p in a) {
                Console.WriteLine(p);
            }
            Console.WriteLine(a.GetType().Name);
            Console.WriteLine(a.Length);
            //Console.WriteLine(a[2]);
            Console.ReadLine();
        }
    }
}
