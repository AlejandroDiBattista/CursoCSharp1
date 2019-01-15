using System;
using System.Collections.Generic;
using System.Text;

namespace Demo5
{
	class Lista<T> where T : IComparable<T>
	{
		List<T> datos = new List<T>();
		public void Agregar(T valor) => datos.Add(valor);

		public T Menor()
		{
			var menor = datos[0];
			foreach (var item in datos)
				if (item.CompareTo(menor) < 0)
					menor = item;
			return menor;
		}
	}

	class Persona : IComparable<Persona>
	{
		public string Nombre { get; set; }
		public int Edad { get; set; }

		public int CompareTo(Persona other)
		{
			if (Edad < other.Edad) return -1;
			if (Edad > other.Edad) return +1;
			return 0;
		}

		public override string ToString() => $"{ Nombre } ({ Edad } años)";
	}
	class Demo
	{
		static void Main(string[] args)
		{
			Console.WriteLine("DEMO Clase genérica con tipo condicionado\n");

			Console.WriteLine("> Colección de ENTEROS");
			var m = new Lista<int>();
			m.Agregar(50);
			m.Agregar(30);
			m.Agregar(20); // <= Menor
			m.Agregar(60);
			m.Agregar(25);

			Console.WriteLine($"El número menor valor es { m.Menor() }");

			Console.WriteLine();
			Console.WriteLine("> Colección de PERSONAS");
			var p = new Lista<Persona>();
			p.Agregar( new Persona() { Nombre = "Alejandro", Edad = 51 } );
			p.Agregar( new Persona() { Nombre = "Andrea",    Edad = 25 } );
			p.Agregar( new Persona() { Nombre = "Francisco", Edad = 35 } );
			p.Agregar( new Persona() { Nombre = "Fernando",  Edad = 26 } );
			p.Agregar( new Persona() { Nombre = "Sergio",    Edad = 33 } );
			p.Agregar( new Persona() { Nombre = "Pablo",	 Edad = 27 } );

			Console.WriteLine($"La Persona mas joven es { p.Menor() }");

			Console.ReadLine();
		}
	}
}
