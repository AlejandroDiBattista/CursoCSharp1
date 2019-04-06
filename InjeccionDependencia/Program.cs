using System;
using System.Collections.Generic;

namespace InjeccionDependencia
{
   class Program
   {
      public interface ICuenta
      {
         void Mostrar();
      }
      class CuentaOro : ICuenta
      {
         public void Mostrar() => Console.WriteLine("cuenta 'ORO'");
      }

      class CuentaPlata : ICuenta
      {
         public void Mostrar() => Console.WriteLine("cuenta 'PLATA'");
      }


      static class Contenedor<Salida>
      {  
         static Dictionary<string, Type> clases = new Dictionary<string, Type>();

         public static void Registrar(string tipo, Type clase)        => clases.Add(tipo, clase);
         public static void Registrar<T>(string tipo) where T: Salida => clases.Add(tipo, typeof(T));

         public static Salida Resolver(string tipo) => (Salida)Activator.CreateInstance(clases[tipo]);
      }

      class Persona
      {
         public string Nombre { get; set; }
         ICuenta cuenta { get; set; }

         public Persona(string nombre, string tipo)
         {
            Nombre = nombre;
            cuenta = Contenedor<ICuenta>.Resolver(tipo);
         }

         public void Mostrar()
         {
            Console.Write($" · Soy '{Nombre}' y tengo una ");
            cuenta.Mostrar();
         }
      }

      interface IAlgo{ };
      class Algo : IAlgo { };

      static void Main(string[] args) {
         Console.WriteLine("DEMOS Injeccion de Dependencia");

         //Contenedor<ICuenta>.Registrar("oro", typeof(CuentaOro));
         //Contenedor.Registrar("plata", typeof(CuentaPlata));

         Contenedor<ICuenta>.Registrar<CuentaOro>("oro");
         Contenedor<ICuenta>.Registrar<CuentaPlata>("plata");

         Contenedor<IAlgo>.Registrar<Algo>("x");

         var a = new Persona("Ale", "oro");
         a.Mostrar();

         var b = new Persona("Fer", "plata");
         b.Mostrar();

         Console.ReadLine(); 
      }
   }
}
