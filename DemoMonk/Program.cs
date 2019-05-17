using System;
using System.Collections.Generic;
using System.Linq;
using Moq;


namespace DemoMonk
{
    class BaseDatos : IBaseDatos
    {
        public void Abrir()
        {
            Console.WriteLine("\n Abrir");
        }

        public void Executar(string SQL)
        {
            Console.WriteLine($" · Ejecutar: {SQL}");
        }

        public IEnumerable<object> Consultar(string SQL)
        {
            Console.WriteLine($" · Consultar: {SQL}");
            yield return null;
        }

        public void Cerrar()
        {
            Console.WriteLine($" Cerrar\n");
        }

    }

    class Persona
    {
        public Guid PersonaId { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
    }

    class PersonaProcesar : IDisposable
    {
        IBaseDatos BD;
        public PersonaProcesar(IBaseDatos db)
        {
            BD = db;
            BD.Abrir();
        }

        public void Crear(Persona persona)
        {
            BD.Executar($"INSERT PERSONA {persona.Nombre}");
        }

        public void Borrar(Persona persona)
        {
            BD.Executar($"DELETE PERSONA {persona.Nombre}");
        }

        public void Dispose()
        {
            BD.Cerrar();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DEMO MoQ!");
            var a = new Persona { PersonaId = Guid.NewGuid(), Nombre = "Alejandro", Edad = 51 };
            var b = new BaseDatos();

            var m = new Mock<IBaseDatos>();

            using (var p = new PersonaProcesar(m.Object))
            {
                p.Crear(a);
                p.Borrar(a);
            }
            Console.Write("Pulsar Enter..."); Console.ReadLine();
        }
    }
}
