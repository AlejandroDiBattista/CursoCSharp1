using System;
using System.Collections.Generic;
using System.Text;

namespace Demo2
{
	// Demo Reutilización CLASE AUXILIAR

	public class Recorrer
	{
		public virtual void Accion(int valor) => Console.WriteLine($" - { valor }");
	}

	public class RecorrerSimpatico : Recorrer
	{
		public override void Accion(int valor) => Console.WriteLine($" :) { valor }");
	}
	public class Pila
	{
		int[] datos = new int[100];
		public int Cantidad { get; private set; } = 0;
		public void Agregar(int valor) => datos[Cantidad++] = valor;
		public int Sacar() => datos[--Cantidad];

		public void Recorrer(Recorrer r)
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

			Console.WriteLine("DEMO RUTILIZACIÓN: Usando Clase Auxiliar con Herencia\n");

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
