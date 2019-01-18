using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DemoEnumerator
{
	class Pares : IEnumerable<int>
	{
		int max;
		public Pares(int max)
		{
			this.max = max;
		}
		public IEnumerator<int> GetEnumerator()
		{
			Console.WriteLine("Comenzo PARES");
			for (int i = 0; ++i < max;)
			{
				Console.WriteLine($"Pares: {i}");
				yield return i * 2;
			}
			Console.WriteLine("Termino PARES");
		}
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
	static class Program
	{
		public static void Display<T>(this IEnumerable<T> lista)
		{
			IEnumerator<T> e = lista.GetEnumerator();
			Console.WriteLine("> RECORRER");
			while (e.MoveNext())
				Console.WriteLine($" - { e.Current }");
			Console.WriteLine(".");
		}

		

		public static IEnumerable<int> Clone(this IEnumerable<int> lista)
		{
			Console.WriteLine("Comenzo CLONE");

			foreach (var i in lista)
			{
				Console.WriteLine($"Clone 1: {i}");
				yield return i;

				Console.WriteLine($"Clone 2: {i}");
				yield return i;
			}
			Console.WriteLine("Termino CLONE");
		}

		public static List<T> ToList<T>(this IEnumerable<T> lista)
		{
			var salida = new List<T>();
			foreach (var item in lista)
			{
				salida.Add(item);
			}
			return salida;
		}
		static void Main(string[] args)
		{
			var a = new List<int>() { 1, 3, 7, 20 };
		
			Console.WriteLine("-----");

			var l = a
				.Where(i => i < 10)
				.Where(i => i > 1)
				.Clone().ToList();

			Console.WriteLine("ANTES DE AGREGAR 2, 9");
			a.Add(2);
			a.Add(9);

			foreach (var item in l)
			{
				Console.WriteLine($" >>> {item} ");
			}
			Console.ReadLine();
		}
	}
}
