using System;
using System.Collections.Generic;
using System.Text;

namespace InjeccionDependencia
{
   class Contador<T>
   {
      static int contar = 0;

      public int Siguiente => contar++;
   }

   class Class1
   {
      static void Main(string[] args)
      {
         var a = new Contador<int>();
         var b = new Contador<double>();

         Console.WriteLine(a.Siguiente);
         Console.WriteLine(a.Siguiente);
         Console.WriteLine(a.Siguiente);
         Console.WriteLine(a.Siguiente);

         Console.WriteLine(b.Siguiente);
         Console.ReadLine();
      }
   }
}
