using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterador2
{
	public interface IEnumerar
	{
		void Reset();
		bool MoveNext();
		int Current { get; }
	}

	public static class Extensiones
	{
		public static void Recorrer(this IEnumerar lista)
		{
			lista.Reset();
			while (lista.MoveNext())
				Console.WriteLine($" - {lista.Current}");
			Console.WriteLine();
		}
	}

	public class Lista : IEnumerar
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
			Console.WriteLine("> Demo 2");

			var lista = TraerLista();

			Console.WriteLine(" 1º Recorrido");
			lista.Recorrer();

			Console.WriteLine(" 2º Recorrido");
			lista.Recorrer();

			Console.ReadLine();
		}
		
		static Lista TraerLista()
		{
			var lista = new Lista();
			lista.Agregar(10);
			lista.Agregar(20);
			lista.Agregar(30);
			return lista;
		}
	}
}
