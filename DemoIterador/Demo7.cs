using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterador
{	
	public class Lista<T> : IEnumerable<T>
	{
		List<T> datos;
		public Lista() => datos = new List<T>();
		public void Agregar(T valor) => datos.Add(valor);

		public IEnumerator<T> GetEnumerator() => datos.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}

	public static class Extensiones {
		public static void Mostrar<T>(this IEnumerable<T> lista)
		{
			Console.WriteLine($" Mostrar { lista.GetType() }");
			lista.Recorrer( t => Console.WriteLine($" . {t}") );
			Console.WriteLine();
		}

		public static void Recorrer<T>(this IEnumerable<T> lista, Action<T> accion)
		{
			foreach (var item in lista)
				accion(item);
		}
		public static IEnumerable<T> Where<T>(this IEnumerable<T> lista, Predicate<T> condicion)
		{
			foreach(var item in lista)
				if (condicion(item))
					yield return item;
		}
		public static IEnumerable<U> Select<T, U>(this IEnumerable<T> lista, Func<T,U> convertir )
		{
			foreach (var item in lista)
				yield return convertir(item);
		}

		public static IEnumerable<U> Map<T,U>(this IEnumerable<T> lista, Func<T,U> convertir)
		{
			foreach (var item in lista)
				yield return convertir(item);
		}
		public static U Reduce<T, U>(this IEnumerable<T> lista, Func<U,T,U> operar)
		{
			var resultado = default(U);
			foreach (var item in lista)
				resultado = operar(resultado, item);
			return resultado;
		}
	}
	public static class DemoEnVivo
	{
		static IEnumerable<int> Pares()
		{
			for (var i = 1; i < 100; i += 2)
				yield return i;
		}

		static IEnumerable<int> Impares()
		{
			for (var i = 1; i < 100; i += 2)
				yield return i;
		}
		public static void Main()
		{
			Console.WriteLine("Demo en VIVO");

			var lista = TraerLista();

			Console.WriteLine("1º Recorrido (mostrar)");
			lista.Mostrar();

			Console.WriteLine("2º Recorrido (flow)");
			lista
				.Where(n => n % 3 == 0)
				.Select(n => n * n)
				.Mostrar();

			Console.WriteLine("3º Recorrido (100 pares % 5)");
			Pares()
				.Where(n => n % 5 == 0)
				.Select(n => n * n)
				.Mostrar();

			var suma = Impares()
				.Map(n => n * 2)
				.Where(n => n % 3 == 0)
				.Reduce((int s, int i) => s + i);

			Console.WriteLine($" Suma (100 impares % 3): {suma}");
			Console.ReadLine();
		}

		static Lista<int> TraerLista()
		{
			var tmp = new Lista<int>();
			tmp.Agregar(10);
			tmp.Agregar(20);
			tmp.Agregar(30);
			tmp.Agregar(40);
			tmp.Agregar(50);
			tmp.Agregar(60);
			tmp.Agregar(70);
			tmp.Agregar(80);
			tmp.Agregar(90);

			return tmp;
		}
	}
}