using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iterador7
{
	public class Lista : IEnumerable<int>
	{
		public List<int> datos;
		//...
		public Lista() => this.datos = new List<int>();
		public void Agregar(int valor) => datos.Add(valor);
		//...
		// Recorrer
		public IEnumerator<int> GetEnumerator() {
			foreach (var i in datos)
				yield return i;
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
	public class Numeros : IEnumerable<int>
	{
		public int Min = 1;
		public int Max = 10;
		public IEnumerator<int> GetEnumerator()
		{
			for(var i = Min; i <= Max; i++)
				yield return i;
		}
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
	public static class Extensiones
	{
		public static void Mostrar(this IEnumerable<int> l)
		{
			Console.WriteLine("> Mostrar");
			l.ForEach(item => Console.WriteLine($" - {item}"));
			Console.WriteLine(".");
		}
		public static void ForEach<T>(this IEnumerable<T> lista, Action<T> action)
		{
			foreach (var item in lista)
				action(item);
		}
		public static IEnumerable<U> Select<T,U>(this IEnumerable<T> lista, Func<T, U> convertir)
		{
			foreach (var item in lista)
				yield return convertir(item);
		}
		public static IEnumerable<T> Where<T>(this IEnumerable<T> lista, Predicate<T> condicion)
		{
			foreach (var item in lista)
			{
				Console.WriteLine($"where: {item} => {condicion(item)}" );
				if (condicion(item))
					yield return item;
			}
		}
		public static List<T> ToList<T>(this IEnumerable<T> lista)
		{
			var resultado = new List<T>();
			foreach (var item in lista)
				resultado.Add(item);
			return resultado;
		}
	}
	public static class Crono
	{
		public static void Main()
		{
			Console.WriteLine("DEMO Crono");

			var l = TraerLista();
			Console.WriteLine("1º Recorrido");
			l.Mostrar();

			Console.WriteLine("2º Recorrido (pares)");
			var p = new Numeros() { Min = 1, Max = 10 };
			var b = l
				.Select(n => n * n)
				.Where(n => n < 500).ToList();

			Console.WriteLine("3º Recorrido (pares)");
			b.Mostrar();

			Console.ReadLine();
		}

		static Lista TraerLista()
		{
			var tmp = new Lista();
			tmp.Agregar(10);
			tmp.Agregar(20);
			tmp.Agregar(30);
			tmp.Agregar(40);
			tmp.Agregar(50);
			return tmp;
		}
	}
}