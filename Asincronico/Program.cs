using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Asincronico
{
   static class Probar
   {

      public static void Otro()
      {
         Thread.Sleep(1000);
         //Console.WriteLine("Salir OTRO");
         return;
      }

      public static async void LentoAsync(int tiempo)
      {
         Console.WriteLine($"    Entra {tiempo}'");               // 1

         await Task.Run(() => Thread.Sleep(tiempo * 1000 ));      // 2 
         Console.WriteLine($"    Sale  {tiempo}'");               // 3

         await Task.Run( Otro );       // 2 
         Console.WriteLine($"    Sale bis {tiempo}'");            // 3

         return ;
      }

      public static void Correr()
      {
         var r = new Stopwatch();
         r.Start();
         Console.WriteLine($" » Comienzo {r.ElapsedMilliseconds,5}ms");

         LentoAsync(1);
         LentoAsync(3);
         LentoAsync(2);

         Console.WriteLine($" » Esperando 2' {r.ElapsedMilliseconds,5}ms");
         //Console.WriteLine($"{a.Result} {b.Result} {c.Result}");
         Console.WriteLine($" » Esperando 3' {r.ElapsedMilliseconds,5}ms");
         //Thread.Sleep(3000);
         //Console.WriteLine($"  >> {a.Result} {b.Result} {c.Result}");
         //Console.WriteLine($" » Fin!     {r.ElapsedMilliseconds,5}ms");
      }
   }
   class Program
   {
      static void Main(string[] args)
      {
         var s = new Stopwatch();
         s.Start();

         Console.WriteLine($"DEMO Asincronico 1! {s.ElapsedMilliseconds}");
         Probar.Correr();
         Console.WriteLine($"Ok                ! {s.ElapsedMilliseconds}");
         Console.Write($"Toque una tecla...");
         Console.ReadLine();

         Console.WriteLine($"DEMO Asincronico 2! {s.ElapsedMilliseconds}");
         Thread.Sleep(1000);
         Console.WriteLine($"Ok                ! {s.ElapsedMilliseconds}");
         Console.ReadLine();
      }
   }
}
