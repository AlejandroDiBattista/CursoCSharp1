using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterador6
{

	public class Lista : IEnumerable<int>
	{
		List<int> datos = new List<int>();
		public Lista()
		{
			datos = new List<int>();
		}
		public void Agregar(int a) => datos.Add(a);

		public IEnumerator<int> GetEnumerator()
		{
			foreach (var i in datos)
				yield return i;
		}

		public IEnumerable<int>GetInvertido()
		{
			var tmp = new List<int>(datos);
			tmp.Reverse();
			return tmp;
		}
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}

	public static class Extensiones
	{
		public static void Recorrer<T>(this IEnumerable<T> lista)
		{
			foreach (var item in lista)
				Console.WriteLine($" - {item}");
			Console.WriteLine();
		}
	}
	class Demo
	{
		static void Main(string[] args)
		{
			Console.WriteLine(">> Demo 6");

			var lista = TraerLista();

			Console.WriteLine(" 1º Recorrido");
			lista.Recorrer();

			Console.WriteLine(" 2º Recorrido");
			lista.GetInvertido().Recorrer();

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
