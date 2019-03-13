using System;

namespace ArbolBinarioNodoSinStatics
{
    public class Arbol
    {
        private class Nodo {
            public int Dato;
            public Nodo Izquierdo, Derecho;
            public Nodo(int valor) => Dato = valor;
            public Nodo Agregar(int valor)
            {
                if (valor < Dato)
                    Izquierdo = Izquierdo?.Agregar(valor) ?? new Nodo(valor);
                else
                    Derecho   = Derecho?.Agregar(valor)   ?? new Nodo(valor);
                return this;
            }
            public void Recorrer()
            {
                Izquierdo?.Recorrer();
                Console.WriteLine($" - {Dato}");
                Derecho?.Recorrer();
            }
            public int Contar => 1 + (Izquierdo?.Contar ?? 0) + (Derecho?.Contar ?? 0);
        }

        private Nodo raiz;

        public void Agregar(int valor) => raiz = (raiz?.Agregar(valor) ?? new Nodo(valor));
        public void Recorrer() => raiz?.Recorrer();
        public int Contar => raiz?.Contar ?? 0;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" > DEMO Arbol Binario (Sin STATICS)");
            var a = new Arbol();
            var r = new Random(1);
            for(var i = 0; i <= 20; i++) {
                a.Agregar( r.Next(10, 99) );
            }
            a.Recorrer();
            Console.WriteLine($" · Hay {a.Contar} valores");
            Console.ReadLine();
        }
    }
}
