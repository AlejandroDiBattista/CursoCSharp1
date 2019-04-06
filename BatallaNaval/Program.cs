using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;

namespace BatallaNaval
{
   enum Estados { Agua, Tiro, Barco, Tocado, Hundido, }


   //if (celda == "·") frente = ConsoleColor.DarkGray;
   //if (celda == "+") frente = ConsoleColor.Yellow;
   //if (celda == "o") frente = ConsoleColor.Blue;
   //if (celda == "#") frente = ConsoleColor.Magenta;
   //if (celda == "X") frente = ConsoleColor.Red;
   //if (celda == "X" || celda == "#" || celda == "o") fondo = ConsoleColor.Gray;

   static class Program
   {
      public static void ForEach<P>(this IEnumerable<P> lista, Action<P> accion)
      {
         foreach (var p in lista)
            accion(p);
      }

      public class Posicion
      {
         public int Fila { get; private set; }
         public int Columna { get; private set; }

         public bool Arriba => Fila == 0 && Columna > 0;
         public bool Izquierda => Fila > 0 && Columna == 0;
         public bool Inicio => Fila == 0 && Columna == 0;
         public bool Borde => Fila == 0 || Columna == 0;

         public Posicion(int fila, int columna)
         {
            Fila = fila;
            Columna = columna;
         }

         public Posicion Desplazar(int v, int h) => new Posicion(Fila + v, Columna + h);
         public bool Igual(Posicion o) => Fila == o.Fila && Columna == o.Columna;
         public static bool operator ==(Posicion a, Posicion b) => a.Igual(b);
         public static bool operator !=(Posicion a, Posicion b) => !a.Igual(b);

         public IEnumerable<Posicion> Recorrer(int alto, int ancho)
         {
            for (var f = 0; f < alto; f++)
               for (var c = 0; c < ancho; c++)
                  yield return Desplazar(f, c);
         }

         public static implicit operator Posicion(string p)
         {
            var f = (int)p[0] - (int)'A' + 1;
            var c = int.Parse(p.Substring(1));
            return new Posicion(f, c);
         }

         public static implicit operator String(Posicion p) => p.ToString();
         public override string ToString() => $"{(char)('A' + Fila + 1)}{Columna}";
      }

      public class Barco
      {
         public Posicion Origen { get; set; }
         public int Ancho { get; set; }
         public int Alto { get; set; }

         public int Toques { get; set; }
         public bool Hundido => Toques == Alto * Ancho;

         public bool Toca(Posicion p) => Origen.Recorrer(Alto, Ancho).Any(a => a == p);

         public Barco(Posicion origen, int tamaño, bool vertical)
         {
            Origen = origen;
            Alto = vertical ? tamaño : 1;
            Ancho = vertical ? 1 : tamaño;
         }

         public static implicit operator Barco(string s)
         {
            var o = s.Substring(0, 2);
            var t = int.Parse(s.Substring(3, 1));
            var v = s.Substring(2, 1) == "V";
            return new Barco(o, t, v);
         }
      }

      class Tablero
      {
         public Posicion Origen { get; private set; }
         public int Tamaño { get; private set; }

         IList<Posicion> disparos = new List<Posicion>();
         IList<Barco> barcos = new List<Barco>();

         public Tablero(int fila, int columna, int tamaño = 8)
         {
            Origen = new Posicion(fila, columna);
            Tamaño = tamaño;
         }
         public void Disparar(params Posicion[] ds) => ds.ForEach(d => disparos.Add(d) );
         public void Ubicar(params Barco[] bs)  => bs.ForEach( b => barcos.Add(b) );

         public IEnumerable<Posicion> Recorrer(Func<Posicion, bool> condicion) => new Posicion(0, 0).Recorrer(Tamaño + 1, Tamaño + 1).Where(condicion);

         Estados Estado(Posicion p)
         {
            var barco   = barcos.SingleOrDefault(b => b.Toca(p));
            var disparo = disparos.SingleOrDefault(d => d == p);

            if (disparo is null && barco is null) return Estados.Agua;
            return barco is null ? Estados.Tiro : (barco.Hundido ? Estados.Hundido : (disparo is null ? Estados.Barco : Estados.Tocado));
         }

         public void Mostrar(Posicion p, String texto, ConsoleColor frente = ConsoleColor.White, ConsoleColor fondo = ConsoleColor.Black)
         {
            Console.CursorLeft = Origen.Columna + p.Columna * 3 + 1;
            Console.CursorTop = Origen.Fila + p.Fila * 1 + 1;
            Console.ForegroundColor = frente;
            Console.BackgroundColor = fondo;
            Console.Write(texto);
         }

         void Mostrar(Posicion p, Estados estado)
         {
            var simbolos = new[] { "·", "+", "o", "#", "X" };
            var colores  = new[] { ConsoleColor.DarkGray, ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.Magenta, ConsoleColor.Red };

            var celda  = simbolos[(int)estado];
            var frente = colores[(int)estado];
            var fondo  = (estado == Estados.Barco || estado == Estados.Tocado || estado == Estados.Hundido) ? (estado == Estados.Hundido ? ConsoleColor.Red : ConsoleColor.Gray) : ConsoleColor.Black;

            Mostrar(p, $" {celda} ", frente, fondo);
         }

         public void Jugadas()
         {
            barcos.ForEach(b => b.Toques = disparos.Count(p => b.Toca(p)));

            Recorrer(p => p.Arriba).ForEach(p => Mostrar(p, $"{p.Columna,2}"));
            Recorrer(p => p.Izquierda).ForEach(p => Mostrar(p, $"{(char)('A' + p.Fila - 1)}"));
            Recorrer(p => !p.Borde).ForEach(p => Mostrar(p, Estado(p)));
         }

         public string Leer(string texto)
         {
            var p = Origen.Desplazar(Tamaño + 2, 0);
            while (true)
            {
               Mostrar(p, "            ");
               Mostrar(p, texto);
               var salida = Console.ReadLine().ToUpper();
               if (salida != "") return salida;
            }
         }
      }

      static void Main(string[] args)
      {
         Console.Title = "Batalla Naval";

         var a = new Tablero(2, 2, 10);
         var b = new Tablero(2, 52, 10);

         b.Disparar("A2", "C3", "C4", "B3", "B5", "B4");

         b.Ubicar("B3H5", "D5V3", "G2H4");

         b.Jugadas();

         Console.OutputEncoding = System.Text.Encoding.UTF8;
         Console.WriteLine("✖");
         for (var i = 1; i <= 4; i++)
         {
            a.Jugadas();
            var barco = a.Leer($"B{i}>");
            a.Ubicar(barco);
         }

         while (true)
         {
            a.Jugadas();
            var d = a.Leer("D>");
            if (d == "") break;
            a.Disparar(d);
         }
      }
   }
}
