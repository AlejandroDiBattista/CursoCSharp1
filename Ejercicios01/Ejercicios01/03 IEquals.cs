using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ejercicios03
{
	class Persona : IEquatable<Persona>
	{
		public string Nombre { get; set; }
		public string Apellido { get; set; }

		public override bool Equals( object other )
		{
			if( ( other == null ) || !( other is Persona ) ) return false;
			return Equals( (Persona)other );
		}
		public bool Equals( Persona other )
		{
			return Nombre == other.Nombre && Apellido == other.Apellido;
		}
		public static bool operator ==( Persona a, Persona b )
		{
			return a.Nombre == b.Nombre && a.Apellido == b.Apellido;
		}
		public static bool operator !=( Persona a, Persona b )
		{
			return !( a == b );
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(Nombre.GetHashCode(), Apellido.GetHashCode());
		}
	}

	class Punto2D : IEquatable<Punto2D>
	{
		public int X { get; set; }
		public int Y { get; set; }

		private Punto2D( int x, int y ) { X = x; Y = y; }

		public static Punto2D Zero() => new Punto2D( 0, 0 );
		public static Punto2D Centro() => new Punto2D( 50, 50 );

		public override bool Equals( object obj ) => ( obj != null ) && ( obj is Punto2D ) && Equals( (Punto2D)obj );
		public bool Equals( Punto2D other ) => X == other.X && Y == other.Y;
		public override int GetHashCode() => HashCode.Combine( X, Y );

		public static bool operator ==( Punto2D a, Punto2D b ) => a.Equals( b );
		public static bool operator !=( Punto2D a, Punto2D b ) => !( a == b );
	}

	class DemoEquals
	{
		static void Main( string[] args )
		{
			Console.WriteLine( "* DEMO IEquatable *\n" );
			Persona p1 = new Persona() { Nombre = "Alejandro", Apellido = "Di Battista" };
			Persona p2 = new Persona() { Nombre = "Alejandro", Apellido = "Di Battista" };
			Persona p3 = p1;

			var a = new int[] { 1, 2, 3, 4 };

			Console.WriteLine( "COMPARACIÓN x REFERENCIA" );
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