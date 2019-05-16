using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Linq;

namespace DemoDinamico
{

    class Punto
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Punto(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    
    class MiPunto : Punto
    {
        Dictionary<string, int> valores = new Dictionary<string, int>();

        public MiPunto(int x, int y) : base(x, y)
        {
            valores["X"] = x;
            valores["Y"] = y;
        }

        public void Set(string nombre, int valor)
        {
            valores[nombre] = valor;
        }
        public int Get(string nombre)
        {
            return valores[nombre];
        }
        public string[] Campos => valores.Keys.ToArray();
        public int[] Valores => valores.Values.ToArray();
    }


    class Program {

        static void Mostrar(Punto p)
        {
            Console.WriteLine($"x: {p.X}, y: {p.Y}");
        }

        static void Mostrar(MiPunto p)
        {
            Console.WriteLine($"campo: {p.Campos}, valores: {p.Valores}");
        }


        static void Main(string[] args)
        {
            var p = new Punto(10,20);
            Mostrar(p);

            var m = new MiPunto(110, 120);
            m.Set("Z", 130);

            Mostrar(m);

            Console.WriteLine(m.Get("X"));
            Console.WriteLine(m.Get("Y"));
            Console.WriteLine(m.Get("Z"));
            Console.ReadLine();
        }
    }
}
