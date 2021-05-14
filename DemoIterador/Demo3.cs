using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterador3
{
	public interface IEnumerar
	{
		void Reset();
		bool MoveNext();
		int Current { get; }
	}

	public interface IEnumerador
	{
		IEnumerar GetEnumerar();
	}
	
	public class Enumerador : IEnumerar
	{
		List<int> datos;
		int posicion;
		public Enumerador(List<int> datos)
		{
			this.datos = new List<int>(datos);
			Reset();
		}
		public void Reset() => posicion = -1;
		public bool MoveNext() => ++posicion < datos.Count;
		public int Current => datos[posicion];
	}

	public class Lista : IEnumerador
	{
		List<int> datos = new List<int>();
		public Lista()
		{
			datos = new List<int>();
		}
		public void Agregar(int a) => datos.Add(a);
		public IEnumerar GetEnumerar() => new Enumerador(datos);
	}

	public static class Extensiones
	{
		public static void Recorrer(this IEnumerador lista) => Recorrer(lista.GetEnumerar());
		public static void Recorrer(this IEnumerar lista)
		{
			lista.Reset();
			while (lista.MoveNext())
				Console.WriteLine($" - {lista.Current}");
			Console.WriteLine();
		}

	}
	
	static class Demo
	{
		static void Main(string[] args)
		{
			Console.WriteLine("> Demo 3");

			var lista = TraerLista();

			Console.WriteLine(" 1º Recorrido");
			lista.Recorrer();

			Console.WriteLine(" 2º Recorrido");
			lista.GetEnumerar().Recorrer();
			
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
