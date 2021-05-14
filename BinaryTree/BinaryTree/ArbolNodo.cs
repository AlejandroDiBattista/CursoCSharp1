using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTree2 {
    public class Arbol {
        static Nodo AgregarNodo(Nodo r, int valor) {
            if (r == null)
                r = new Nodo(valor);
            else if (valor < r.Dato) {
                r.Menor = AgregarNodo(r.Menor, valor);
            } else {
                r.Mayor = AgregarNodo(r.Mayor, valor);
            }
            return r;
        }

        private class Nodo {
            public int Dato;
            public Nodo Menor, Mayor;
            public Nodo(int valor) => Dato = valor;
        }

        Nodo raiz;
        public void Agregar(int valor) {
            raiz = AgregarNodo(raiz, valor);
        }
    }
}
