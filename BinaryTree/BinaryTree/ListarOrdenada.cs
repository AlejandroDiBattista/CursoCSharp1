using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTree
{
    class ListarOrdenada<T> where T:IComparable
    {
        List<T> datos;
        public ListarOrdenada(){
            datos = new List<T>();
        }
        public void Agregar(T valor)
        {
            var i = datos.BinarySearch(valor);
            datos.Insert(i < 0 ? -i : i, valor);
        }
        public bool Contiene(T valor) => datos.BinarySearch(valor) >= 0;
    }
}
