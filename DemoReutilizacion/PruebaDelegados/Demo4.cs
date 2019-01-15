using System;
using System.Collections.Generic;
using System.Text;

namespace Demo4
{
	// Demo Reutilización DELEGADO

	public delegate void Accion(int valor);

	public class Pila
	{
		int[] datos = new int[100];
		public int Cantidad { get; private set; } = 0;
		public void Agregar(int valor) => datos[Cantidad++] = valor;
		public int Sacar() => datos[--Cantidad];

		//public void Recorrer(Action <int> accion) // <= Declaracion alternativa usando Delegado Generico
		public void Recorrer(Accion accion)
		{
			for (var i = 0; i < Cantidad; i++)
				accion(datos[i]); // <= Llama al delegado
		}
	}

	class Demo
	{
		static void MostrarConsola(int value) => Console.WriteLine($" - { value } ");

		static void Main(string[] args)
		{
			Pila p = new Pila();
			p.Agregar(10);
			p.Agregar(20);
			p.Agregar(30);
			p.Agregar(40);
			p.Agregar(50);

			Console.WriteLine("DEMO RUTILIZACIÓN: Usando Delegados\n");

			Console.WriteLine("1. Recorre la pila!");

			p.Recorrer(MostrarConsola);
			// p.Recorrer( new Accion(MostrarConsola) ); // <= Forma 'Completa' de pasar un delgado
			Console.WriteLine();

			Console.WriteLine("2. Recorre la pila (Simpatica) (usando funcion Lambda)!");
			p.Recorrer(valor => Console.WriteLine($" :) {valor } " ));
			Console.WriteLine();

			Console.WriteLine("Saca todos los elementos");
			while (p.Cantidad > 0)
				Console.WriteLine($" - { p.Sacar() }");

			Console.ReadLine();
		}
	}
}