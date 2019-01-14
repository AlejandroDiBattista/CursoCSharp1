using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterador5
{
	public class Enumerador<T> : IEnumerator<T> where T : new()
	{
		List<T> datos;
		int posicion;
		public Enumerador(List<T> datos)
		{
			this.datos = new List<T>(datos);
			Reset();
		}
		public void Reset() => posicion = -1;
		public bool MoveNext() => ++posicion < datos.Count;
		public T Current => datos[posicion];
		// Funciones extras por compatibilidad
		object IEnumerator.Current => this.Current;
		void IDisposable.Dispose() { }
	}

	public class Lista : IEnumerable<int>
	{
		List<int> datos = new List<int>();
		public Lista()
		{
			datos = new List<int>();
		}
		public void Agregar(int a) => datos.Add(a);

		public IEnumerator<int> GetEnumerator() => new Enumerador<int>(datos);

		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}

	public static class Extensiones { 
		public static void Recorrer<T>(this IEnumerable<T> lista)
		{
			foreach(var item in lista)
				Console.WriteLine($" - {item}");
			Console.WriteLine();
		}
	}
	class Demo
	{
		static void Main(string[] args)
		{
			Console.WriteLine("> Demo 5");

			var lista = TraerLista();
			
			Console.WriteLine(" 1º Recorrido");
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
