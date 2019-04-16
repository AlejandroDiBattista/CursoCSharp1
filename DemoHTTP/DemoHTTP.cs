using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Demo
{
   class DemoHTTP
   {
      static void Main()
      {
         Console.SetWindowSize(100, 30);
         Console.WriteLine("DEMO HTTP");
         Task t = new Task(DownloadPageAsync);
         t.Start();
         Console.WriteLine("Downloading page...");
         Console.ReadLine();
      }

      static async void DownloadPageAsync()
      {
         string page = "http://www.wordreference.com/definicion/perro";
         var handler = new HttpClientHandler();
         if (handler.SupportsAutomaticDecompression)
         {
            handler.AutomaticDecompression =   DecompressionMethods.GZip | DecompressionMethods.Deflate;
         }
         HttpClient client = new HttpClient(handler);
         //client.DefaultRequestHeaders.Add("Nombre", "Alejandro");
         client.DefaultRequestHeaders.Add("User-Agent", @"C# App");
         Console.WriteLine(client.DefaultRequestHeaders);
         Console.ReadLine();
         HttpResponseMessage response = await client.GetAsync(page);
         HttpContent content = response.Content;

         Console.WriteLine();
         Console.WriteLine("-[HEAD]--------------");
         foreach (var h in response.Headers)
         {
            var texto = String.Join(" | ", h.Value).PadRight(100);
            if (texto.Length > 100)
            {
               texto = texto.Substring(0, 97) + "...";
            }
            Console.WriteLine($" {h.Key,-25}: {texto}");

         }
         Console.WriteLine();
         Console.WriteLine("-[BODY]--------------");
         string result = await content.ReadAsStringAsync();

         if(result.Contains("perro"))
         {
            Console.WriteLine("SOMOS UNOS GENIOS (ponele)");
            Console.ReadLine();
         }
         if (result != null && result.Length >= 50)
            Console.WriteLine(result + "...");
      }
   }
}