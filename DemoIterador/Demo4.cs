using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iterador4
{
	public interface IEnumerar<T>
	{
		void Reset();
		bool MoveNext();
		T Current { get; }
	}

	public interface IEnumerador<T>
	{
		IEnumerar<T> GetEnumerar();
	}

	public class Enumerador<T> : IEnumerar<T> where T : new()
	{
		List<T> datos;
		int posicion;
		public Enumerador(List<T> datos)
		{
			this.datos= new List<T>(datos);
			Reset();
		}
		public void Reset() => posicion = -1;
		public bool MoveNext() => ++posicion < datos.Count;
		public T Current => datos[posicion];
	}
	public class EnumeradorInvertido<T> : IEnumerar<T> where T : new()
	{
		List<T> copia;
		int posicion;
		public EnumeradorInvertido(List<T> datos)
		{
			copia = new List<T>();
			copia.AddRange(datos);
			Reset();
		}
		public void Reset() => posicion = copia.Count;
		public bool MoveNext() => --posicion >= 0;
		public T Current => copia[posicion];
	}

	public class Lista : IEnumerador<int>
	{
		List<int> datos = new List<int>();
		public Lista()
		{
			datos = new List<int>();
		}
		public void Agregar(int a) => datos.Add(a);

		public IEnumerar<int> GetEnumerar() => new Enumerador<int>(datos);
		public IEnumerar<int> Invertido() => new EnumeradorInvertido<int>(datos);
	}

	public static class Extensiones { 
		public static void Recorrer<T>(this IEnumerador<T> lista) => Recorrer(lista.GetEnumerar());
		public static void Recorrer<T>(this IEnumerar<T> lista)
		{
			while (lista.MoveNext())
				Console.WriteLine($" - {lista.Current}");
			Console.WriteLine();
		}
	}

	static class Demo
	{
		static void Main(string[] args)
		{
			Console.WriteLine("> Demo 4");

			var lista = TraerLista();

			Console.WriteLine(" 1º Recorrido");
			lista.Recorrer();

			Console.WriteLine(" 2º Recorrido (Invertido)");
			lista.Invertido().Recorrer();
			
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
