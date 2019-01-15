using System;

namespace Demo1
{
	// Demo Reutilización USANDO HERENCIA

	public class Pila
	{
		int[] datos = new int[100];
		public int Cantidad { get; private set; } = 0;
		public void Agregar(int valor) => datos[Cantidad++] = valor;
		public int Sacar() => datos[--Cantidad];

		public virtual void Accion(int i) => Console.WriteLine(i);

		public void Recorrer()
		{
			for (var i = 0; i < Cantidad; i++)
				Accion(datos[i]);
		}
	}

	public class PilaSimpatica : Pila
	{
		public override void Accion(int i) => Console.WriteLine($" :) {i} ");
	}

	class Demo
	{
		static void Main(string[] args)
		{
			Pila p = new PilaSimpatica();
			// Pila p = new Pila();  // Descomentar para probar la Pila Original
			p.Agregar(10);
			p.Agregar(20);
			p.Agregar(30);
			p.Agregar(40);
			p.Agregar(50);

			Console.WriteLine("DEMO RUTILIZACIÓN: Usando Herencia\n");

			Console.WriteLine("Recorre la pila!");
			p.Recorrer();
			Console.WriteLine();

			Console.WriteLine("Saca todos los elementos");
			while (p.Cantidad > 0)
				Console.WriteLine($" - { p.Sacar() }");

			Console.ReadLine();
		}
	}
}
