using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ArbolBinarioEnumerable
{
    public class Arbol : IEnumerable<int>
    {
        private int? dato;
        private Arbol izquierda, derecha;

        public void Agregar(int valor)
        {
            if(dato == null) {
                dato = valor;
            } else if(valor < dato) {
                if (izquierda == null) izquierda = new Arbol();
                izquierda.Agregar(valor);
            } else {
                ( derecha ?? (derecha = new Arbol()) ).Agregar(valor);
            }
        }
        public void Recorrer(int nivel) {
            izquierda?.Recorrer(nivel+1);
            if (dato != null) Console.WriteLine($" { new String(' ', nivel*3) }- {dato}");
            derecha?.Recorrer(nivel +1);
        }
        public void Recorrer()
        {
            izquierda?.Recorrer();
            if (dato != null) Console.WriteLine($" - {dato}");
            derecha?.Recorrer();
        }
        public void RecorrerInterativo()
        {
            if (dato == null) return;

            var q = new Stack<Arbol>();
            Arbol a = this;
            while (q.Count > 0 || a != null) {
                if(a != null) {
                    q.Push(a);
                    a = a.izquierda;
                }
                else {
                    a = q.Pop();
                    Console.WriteLine($" - {a.dato}");
                    a = a.derecha;
                }
            } 
        }

        public IEnumerator<int> GetEnumerator()
        {
            if (dato == null) yield break;

            var q = new Stack<Arbol>();
            Arbol a = this;

            while (q.Count > 0 || a != null)
            {
                if (a != null)
                {
                    q.Push(a);
                    a = a.izquierda;
                }
                else
                {
                    a = q.Pop();
                    yield return (int)a.dato;
                    a = a.derecha;
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();

        public int Contar => this.Count();
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" > DEMO Arbol Binario");
            var a = new Arbol();
            var r = new Random(1);
            for(var i = 0; i <= 20; i++) {
                a.Agregar( r.Next(10, 99) );
            }
            Console.WriteLine(" > RECORRE NORMAL");
            a.Recorrer();

            Console.WriteLine(" > RECORRE CON NIVEL");
            a.Recorrer(1);

            Console.WriteLine(" > RECORRE SIN RECURSIVIDAD");
            a.RecorrerInterativo();

            Console.WriteLine(" > RECORRE CON ENUMERACION");

            foreach (var dato in a) {
                Console.WriteLine($" - {dato}");
            }

            Console.WriteLine($" · Hay {a.Contar} valores");
            Console.ReadLine();
        }
    }
}
