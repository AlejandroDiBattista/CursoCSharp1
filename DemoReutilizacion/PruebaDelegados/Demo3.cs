using System;
using System.Collections.Generic;
using System.Text;

namespace Demo3
{
	// Demo Reutilización CLASE AUXILIAR + INTERFACE

	public interface IRecorrer
	{
		void Accion(int valor);
	}
	public class Recorrer : IRecorrer 
	{
		public void Accion(int valor) => Console.WriteLine($" - { valor }");
	}

	public class RecorrerSimpatico : IRecorrer // <= Ya no hereda de Recorrer
	{
		public void Accion(int valor) => Console.WriteLine($" :) { valor }");
	}

	public class Pila
	{
		int[] datos = new int[100];
		public int Cantidad { get; private set; } = 0;
		public void Agregar(int valor) => datos[Cantidad++] = valor;
		public int Sacar() => datos[--Cantidad];

		public void Recorrer(IRecorrer r)
		{
			for (var i = 0; i < Cantidad; i++)
				r.Accion(datos[i]);
		}
	}

	class Demo
	{
		static void Main(string[] args)
		{
			Pila p = new Pila();
			p.Agregar(10);
			p.Agregar(20);
			p.Agregar(30);
			p.Agregar(40);
			p.Agregar(50);
			Console.WriteLine("DEMO RUTILIZACIÓN: Usando Interface en Clases auxiliares)\n");

			Console.WriteLine("1. Recorre la pila!");
			var r = new Recorrer();
			p.Recorrer(r);
			Console.WriteLine();

			Console.WriteLine("2. Recorre la pila (Simpatica)!");
			var rs = new RecorrerSimpatico();
			p.Recorrer(rs);
			Console.WriteLine();

			Console.WriteLine("Saca todos los elementos");
			while (p.Cantidad > 0)
				Console.WriteLine($" - { p.Sacar() }");

			Console.ReadLine();
		}
	}
}