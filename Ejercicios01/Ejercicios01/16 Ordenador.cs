using System;
using System.Collections.Generic;
using System.Text;

namespace Ejercicios16
{
	static class Ordenador
	{

		public class Persona 
		{
			public string Nombre { get; set; }
			public string Apellido { get; set; }
			public int Edad { get; set; }

		}

		public class Comparador
		{
			public int Comparar( Persona a, Persona b )
			{
				var comparar = 0;

				if( comparar == 0 ) comparar = a.Apellido.CompareTo( b.Apellido );
				if( comparar == 0 ) comparar = a.Nombre.CompareTo( b.Nombre );
				if( comparar == 0 ) comparar = -a.Edad.CompareTo( b.Edad );

				return comparar;
			}
		}


		static List<Persona> Ordenar( IEnumerable<Persona> datos, Comparador c ) 
		{
			var lista = new List<Persona>( datos );

			for( var i = 0; i < lista.Count - 1; i++ ) {
				for( var j = i + 1; j < lista.Count; j++ ) {
					if(  c.Comparar(lista[ j ], lista[ i ]) < 0 ) {
						var tmp = lista[ i ];
						lista[ i ] = lista[ j ];
						lista[ j ] = tmp;
					}
				}
			}
			return lista;
		}

		static void Main( string[] args )
		{
			var datos = new List<Persona> {
				new Persona{ Nombre = "Alejandro", Apellido = "Di Battista", Edad = 51 },
				new Persona{ Nombre = "Mirta",     Apellido = "Peroña",      Edad = 72 },
				new Persona{ Nombre = "Franco",    Apellido = "Di Battista", Edad = 14 },
				new Persona{ Nombre = "Mirta",     Apellido = "Peroña",      Edad = 50 },
				new Persona{ Nombre = "Maira",     Apellido = "Di Battista", Edad = 19 },
			};

			var c = new Comparador();

			Ordenar( datos, c ).ForEach( Console.WriteLine );
			Console.WriteLine();
			Ordenar( datos, c ).ForEach( Console.WriteLine );
			Console.WriteLine(Sumar(1,2));
			var s = new Sumador( 1, 2 );
			Console.WriteLine( s.Sumar() );

			Console.ReadLine();
		}
		public static int Sumar( int a, int b ) => a + b;
		class Sumador
		{
			public int A { get; set; }
			public int B{ get; set; }

			public Sumador(int a, int b )
			{
				A = a;
				B = b;
			}
			public int Sumar() => A + B;

		}
	}
}
