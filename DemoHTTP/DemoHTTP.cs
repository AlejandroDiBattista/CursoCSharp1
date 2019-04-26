using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace Demo
{
   class DemoHTTP
   {
      public static IEnumerable<string> Bajar(string palabra)
      {
         var url = $"http://www.wordreference.com/definicion/{palabra}";

         HtmlWeb web = new HtmlWeb();
         var htmlDoc = web.Load(url);
         var nodes = htmlDoc.DocumentNode.SelectNodes(@"//ol/li");

         foreach (var node in nodes)
            yield return node.InnerHtml;
      }

      static void Mostrar(string[] textos)
      {
         foreach (var partes in textos)
         {
            switch (partes)
            {
               case @"<br>":
                  Console.WriteLine();
                  break;
               case @"<span class=""i"">": Console.ForegroundColor = ConsoleColor.DarkGreen;
                  break;
               case @"<span class=""b"">":
                  Console.ForegroundColor = ConsoleColor.White;
                  break;
               case @"</span>":
                  Console.ForegroundColor = ConsoleColor.DarkGray;
                  break;
               default:
                  Console.Write(partes);
                  break;
            }
         }
         Console.WriteLine();
         Console.WriteLine();
      }
      static void Main()
      {
         Console.SetWindowSize(120, 30);
         Console.Title = "DEMO HTTPClient";
         var r = new Regex(@"(<[^<>]*>)", RegexOptions.IgnoreCase);
         var i = 1;
         foreach (var item in Bajar("gato"))
         {
            Console.Write($"{i++}. ");
            Mostrar(r.Split(item));
         }

         Console.ReadLine();
      }

      static async Task<string> BajarPagina(string palabra)
      {
         string url = $"http://www.wordreference.com/definicion/{palabra}";

         var handler = new HttpClientHandler();
         if (handler.SupportsAutomaticDecompression)
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

         var client = new HttpClient(handler);
         client.DefaultRequestHeaders.Add("User-Agent", @"C# App");

         var response = await client.GetAsync(url);
         return await response.Content.ReadAsStringAsync();
      }
   }
}