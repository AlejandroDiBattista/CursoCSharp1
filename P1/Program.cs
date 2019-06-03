using System;
using System.Linq;
using System.Collections.Generic;

namespace P1
{
    interface ICalculable<T>
    {
        T Suma(T otro);
        T Resta(T otro);
        T Producto(T otro);
    }

    class Flotante : ICalculable<float>
    {
        public float Producto(float otro)
        {
            throw new NotImplementedException();
        }

        public float Resta(float otro)
        {
            throw new NotImplementedException();
        }

        public float Suma(float otro)
        {
            throw new NotImplementedException();
        }
    }


    class Integer : ICalculable<int>
    {
        public int Producto(int otro)
        {
            throw new NotImplementedException();
        }

        public int Resta(int otro)
        {
            throw new NotImplementedException();
        }

        public int Suma(int otro)
        {
            throw new NotImplementedException();
        }
    }
    interface ISumable<T>
    {
        T Sumar(T a, T b);
    }

    class PuntoBase<T> 
    {
        public T X { get; set; }
        public T Y { get; set; }

    }

    class Punto : PuntoBase<int>
    {
        internal int Z { get; set; }

        public Punto(int x, int y)
        {
            X = x;
            Y = y;
            Z = x + y;
        }

        public override string ToString()
        {
            return $"(X:{X}, Y:{Y})";
        }

        public override bool Equals(object obj)
        {
            if(obj is Punto otro){
                return this.Z == otro.Z;
            }
            return false;
        }

        public Punto Sumar(Punto otro)
        {
            return new Punto(X + otro.X, Y + otro.Y);
        }

        public static Punto operator +(Punto a, Punto b) => a.Sumar(b);
    }

    class PuntoExtra : Punto
    {
        public PuntoExtra(int m) : base(m, m)
        {
            this.Z = m * 100;
        }
    }

    static class Program
    {

        public static Punto Sum(this IEnumerable<Punto> puntos) => 
            puntos.Aggregate(new Punto(0, 0), (s, e) => s = s.Sumar(e));

        static void Main(string[] args)
        {
            var p = new[] { new Punto(10, 20), new Punto(30, 40) };

            var a = new Punto(10, 20);
            var b = new Punto(10, 20);
            var c = a + b;

            a.Z = 10;

            Console.WriteLine($"Hola punto {a}!");
            Console.WriteLine($"Hola punto {b}!");
            Console.WriteLine($"Hola punto {c}!");
            Console.WriteLine($"a==b > {a.Equals(b)}");
            Console.WriteLine($"{p.Sum()}");
            Console.ReadLine();
        }
    }
}
