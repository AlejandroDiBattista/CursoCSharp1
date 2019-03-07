using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ejercicios20
{
    public static class Extensiones
    {
        public static String[] Palabras(this string texto) => texto.Split(' ');

        public static bool PalabraComienzaCon(this string texto, string filtro) => 
            filtro.Palabras().All(
                palabra => texto.Palabras().Any(
                    p => p.StartsWith(palabra)));
    }

    public interface Listador
    {
        void Comenzar();
        void Mostrar(int item);
        void Finalizar();
    }

    public class ListadorSimple : Listador
    {
        public void Comenzar() => Console.WriteLine("LISTADOR SIMPLE");
        public void Mostrar(int item) => Console.WriteLine($" - {item}");
        public void Finalizar() => Console.WriteLine(".");
    }
    public class ListadorOrdenado : Listador
    {
        int linea = 0;
        public void Comenzar() => Console.WriteLine("LISTADOR NUMERADO");
        public void Mostrar(int item) => Console.WriteLine($" {++linea}) {item}");
        public void Finalizar() => Console.WriteLine("FIN");
    }

    public class Burbuja
    {
        public int[] Datos { get; private set; }
        public Burbuja(int[] datos)
        {
            Datos = datos;
        }
        public void Ordenar()
        {
            for(var i = 0; i < Datos.Length-1; i++)
            {
                for(var j = i+1; j < Datos.Length; j++)
                {
                    if(! OrdenCorrecto(i,j))
                    {
                        Intercambiar(i, j);
                    }
                }
            }
        }

        public virtual bool OrdenCorrecto(int i, int j) => Datos[i] < Datos[j];
        public virtual void Intercambiar(int i, int j)
        {
            var tmp = Datos[i];
            Datos[i] = Datos[j];
            Datos[j] = tmp;
        }
        public void Listar(Listador l)
        {
            Ordenar();
            l.Comenzar();
            foreach (var item in Datos)
            {
                l.Mostrar(item);
            }
            l.Finalizar();
        }

        public class BurbujaInversa : Burbuja
        {
            public BurbujaInversa(int[] datos) : base(datos) { }
            public override bool OrdenCorrecto(int i, int j) => Datos[i] > Datos[j];
        }

        static void Main(string[] args)
        {
            var datos = new[] { 10, 30, 50, 70, 20, 90, 40, 60, 80 };
            var b = new BurbujaInversa(datos);
            b.Ordenar();
            b.Listar(new ListadorSimple());
            b.Listar(new ListadorOrdenado());
            Console.ReadLine();
        }
    }
}
