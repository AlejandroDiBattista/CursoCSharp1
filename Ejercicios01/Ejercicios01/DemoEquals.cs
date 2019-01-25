using System;
using System.Collections.Generic;
using System.Text;

namespace Ejercicios06
{

	public class Persona//: IEquatable<Persona>
	{
		public string Nombre { get; set; }
		public string Apellido { get; set; }

		//public bool Equals( Persona other )
		//{
		//	return Nombre == other.Nombre && Apellido == other.Apellido;
		//}
		//public static bool operator ==(Persona a, Persona b )
		//{
		//	return a.Nombre == b.Nombre && a.Apellido == b.Apellido;
		//}
		//public static bool operator !=( Persona a, Persona b )
		//{
		//	return !( a == b );
		//}
	}

	class Program
{
		static void Main( string[] args )
		{
			Persona p1 = new Persona() { Nombre = "Alejandro", Apellido = "Di Battista" };
			Persona p2 = new Persona() { Nombre = "Alejandro", Apellido = "Di Battista" };
			Persona p3 = p1;

			Console.WriteLine( "COMPARACION x REFERENCIA" );
			Console.WriteLine();
			Console.WriteLine( $" p1 == p2 > {p1 == p2}" );
			Console.WriteLine( $" p1 == p3 > {p1 == p3}" );
			Console.WriteLine();
			Console.WriteLine( $" p1.equals(p2) > {p1.Equals( p2 )}" );
			Console.WriteLine( $" p1.equals(p3) > {p1.Equals( p3 )}" );
			Console.WriteLine();
			Console.WriteLine( $" Object.Equals(p1, p2) > {Object.Equals( p1, p2 )}" );
			Console.WriteLine( $" Object.Equals(p1, p3) > {Object.Equals( p1, p3 )}" );

			Console.ReadLine();
		}

	}
}
