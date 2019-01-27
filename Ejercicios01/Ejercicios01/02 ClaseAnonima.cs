using System;
using System.Collections.Generic;
using System.Text;

namespace Ejercicios02
{
	class Persona
	{
		public string Nombre { get; set; }
		public int Edad { get; set; }
		public override string ToString() => $"{{ Nombre = {Nombre}, Edad = {Edad} }}";
	}
	struct Punto
	{
		public int X { get; set; }
		public int Y { get; set; }
		public override string ToString() => $"(X:{X}, Y:{Y})";
	}
	class DemoClaseAnonima
	{

		static (int, int) PuntoInicial()
		{
			var x = 0;
			var y = 0;
			return (x, y);
		}

		static void Main( string[] args )
		{
			Console.WriteLine( "* DEMO Clase Anónima *\n" );

			object o = new Persona() { Nombre = "Ale", Edad = 49 };
			Console.WriteLine( o );

			var a = new { Nombre = "Alejandro", Edad = 51 };
			var b = new Persona() { Nombre = "Tristan", Edad = 49 };
			(string Nombre, int Edad) c;
			c.Nombre = "Gonzalo";
			c.Edad = 37;

			Tuple<string, int> d = new Tuple<string, int>( "Jesus", 33 );
			//var e = (Nombre: "Jose", Edad: 56);

			var Nombre = "XXX";
			var Edad = 10;

			var p = (X: 10, Y: 20);
			Console.WriteLine( p );
			var f = new { Nombre, Edad };
			var i = (Nombre, AlgoRaro: Edad);

			Console.WriteLine( "Clase Anonima" );
			Console.WriteLine( a.GetType() );
			//Console.WriteLine( b.ToString() );
			//Console.WriteLine( c.ToString() );
			//Console.WriteLine( d.ToString() );
			//Console.WriteLine( e.ToString() );
			Console.WriteLine( f.Nombre );
			Console.WriteLine( i.AlgoRaro );

			Console.ReadLine();
		}

	}
}
