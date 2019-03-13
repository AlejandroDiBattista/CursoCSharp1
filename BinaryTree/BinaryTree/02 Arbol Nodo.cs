using System;

namespace ArbolBinarioNodo
{
    public class Arbol
    {
        private class Nodo {
            public int Dato;
            public Nodo Izquierdo, Derecho;
            public Nodo(int valor) => Dato = valor;
            public static void Agregar(ref Nodo nodo, int valor)
            {
                if(nodo == null)
                    nodo = new Nodo(valor) ;
                else if(valor < nodo.Dato)
                    Agregar(ref nodo.Izquierdo, valor);
                else
                    Agregar(ref nodo.Derecho, valor);
            }
            
            public static void Recorrer(Nodo nodo){
                if (nodo == null) return;

                Nodo.Recorrer(nodo.Izquierdo);
                Console.WriteLine($" - {nodo.Dato}");
                Nodo.Recorrer(nodo.Derecho);
            }

            public static int Contar(Nodo nodo) => nodo == null ? 0 : (1 + Nodo.Contar(nodo.Izquierdo) + Nodo.Contar(nodo.Derecho)); 
        }

        private Nodo raiz;
        public void Agregar(int valor) => Nodo.Agregar(ref raiz, valor); 
        public void Recorrer() => Nodo.Recorrer(raiz); 
        public int Contar => Nodo.Contar(raiz);
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" > DEMO Arbol Binario (CON Static)");
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
