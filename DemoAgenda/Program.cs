using System;
using Newtonsoft.Json;
using System.Linq;
using System.IO;

namespace DemoAgenda
{
    class Program
    {
        static void Main(string[] args)
        {
            var agenda = new Agenda();
            agenda.Agregar(new Contacto("Di Battista", "Alejandro", email: "alejandrodibattista@gmail.com", telefono: "3815343458"));
            agenda.Agregar(new Contacto("Di Battista", "Franco",    email: "franquitodibattista@gmail.com", telefono: "3815216946"));

            Almacen alma; 
            Console.WriteLine("DEMO EN MEMORIA");
            Console.WriteLine(" - Serializando...");
            var escribir = new StringWriter();
            alma = new Almacen(null, escribir);
            alma.Escribir(agenda);

            var json = escribir.ToString();
            //Console.WriteLine(json);

            var leer = new StringReader(json);
            alma = new Almacen(leer, null);

            Console.WriteLine(" - DESerializando...");
            var nueva_agenda = alma.Leer();

            Mostrar(nueva_agenda);
            Console.ReadLine();

            Console.WriteLine("DEMO EN ARCHIVO");
            Console.WriteLine(" - Serializando...");

            alma = new Almacen(@"..\..\..\agenda.json");
            alma.Escribir(agenda);

            Console.WriteLine(" - DESerializando...");
            nueva_agenda = alma.Leer();

            Mostrar(nueva_agenda);
            Console.ReadLine();

        }
        static void Mostrar(Agenda agenda)
        {
            Console.WriteLine( $"Listado de AGENDA (Hay {agenda.Cantidad})");
            var i = 0;
            foreach (var contacto in agenda.BuscarTodos("gmail"))
                Console.WriteLine($"{++i}) {contacto}");

        }
    }
}
