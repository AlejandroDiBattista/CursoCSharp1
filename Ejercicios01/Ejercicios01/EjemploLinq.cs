using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ejercicios04
{
	static class EjemploLinq
	{
		public class Persona
		{
			public string Nombre { get; set; }
			public int Edad { get; set; }
			public bool Hombre { get; set; }
			public override string ToString() => $"Nombre: {Nombre,-10} Edad: {Edad,2} Sexo: {( Hombre ? "H" : "M" ) }";
		}

		static void Mostrar<T>( this IEnumerable<T> lista, string mensaje = "" )
		{
			Console.WriteLine( $" > {mensaje} " );
			foreach( var item in lista ) {
				Console.WriteLine( $" · { item }" );
			}
			Console.WriteLine();
		}



		static void Main( string[] args )
		{
			Console.WriteLine( "DEMOSTRACION LinQ" );

			var salida = from p in TraerPersonas() where p.Hombre && p.Edad < 50 select new { p.Nombre, p.Edad };
			foreach( var p in salida ) {
				Console.WriteLine( p );
			}
			Console.ReadLine();
		}

		static IEnumerable<Persona> TraerPersonas()
		{
			yield return new Persona() { Nombre = "Alejandro", Edad = 50, Hombre = true };
			yield return new Persona() { Nombre = "Juan", Edad = 40, Hombre = true };
			yield return new Persona() { Nombre = "Jose", Edad = 30, Hombre = true };
			yield return new Persona() { Nombre = "Alejandra", Edad = 30, Hombre = false };
			yield return new Persona() { Nombre = "Juana", Edad = 40, Hombre = false };
			yield return new Persona() { Nombre = "Josefa", Edad = 50, Hombre = false };
			yield return new Persona() { Nombre = "Mario", Edad = 20, Hombre = true };
			yield return new Persona() { Nombre = "Maria", Edad = 10, Hombre = false };
		}
	}
}
