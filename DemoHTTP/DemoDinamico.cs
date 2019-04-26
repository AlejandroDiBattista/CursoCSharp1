using System;
using System.Collections.Generic;
using System.Text;

namespace Demo
{
   class DemoDinamico
   {
      class Animal
      {
         public virtual void Hablar() => Console.WriteLine(":)");
      }

      class Perro : Animal
      {
         public override void Hablar() => Console.WriteLine("GUAU");
         public void Ladrar() => Console.WriteLine("Estoy LADRANDO");
      }

      class Gato : Animal
      {
         public override void Hablar() => Console.WriteLine("MIAU");
         public void Ronronear() => Console.WriteLine("Estoy RONRONEANDO");
      }

      static void Mostrar(Animal a) => a.Hablar();

      static void Ejecutar(Animal a)
      {
         Console.Write("Soy un animal y ...");
         if (a is Perro p) Ejecutar(p);
         if (a is Gato  g) Ejecutar(g);
      }

      static void Ejecutar(Perro p)
      {
         Console.Write("Soy un PERRO y ...");
         p.Ladrar();
      }

      static void Ejecutar(Gato g)
      {
         Console.Write("Soy un GATO y ...");
         g.Ronronear();
      }

      static void Main(string[] args)
      {
         var a = new Perro();
         
         //Mostrar(a);
         Ejecutar((dynamic)a);

         //a = new Perro();
         //Ejecutar(a);

         Ejecutar(a);
         Console.ReadLine();
      }
   }
}
