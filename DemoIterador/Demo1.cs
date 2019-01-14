using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterador1
{
	class Lista
	{
		List<int> datos = new List<int>();
		public Lista()
		{
			datos = new List<int>();
			Reset();
		}
		public void Agregar(int a) => datos.Add(a);
		
		// Iterar
		int posicion;
		public void Reset() => posicion = -1;
		public bool MoveNext() => ++posicion < datos.Count;
		public int Current => datos[posicion];
	}
	class Demo
	{
		static void Main(string[] args)
		{
			Console.WriteLine(">> Demo 1");

			var lista = new Lista();
			lista.Agregar(10);
			lista.Agregar(20);
			lista.Agregar(30);

			Console.WriteLine(" 1º Recorrido");
			while( lista.MoveNext() )
				Console.WriteLine($" - { lista.Current }");
			Console.WriteLine();

			lista.Reset();
			Console.WriteLine(" 2º Recorrido");
			while( lista.MoveNext() )
				Console.WriteLine($" - { lista.Current }");
			Console.WriteLine();

			Console.ReadLine();
		}
	}
}
