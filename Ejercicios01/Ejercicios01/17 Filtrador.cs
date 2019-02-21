using System;
using System.Collections.Generic;
using System.Text;

namespace Ejercicios17
{
	static class Filtrador
	{
		public class Persona
		{
			public string Nombre { get; set; }
			public string Apellido { get; set; }
			public int Edad { get; set; }
			public bool EsHombre { get; set; }
			public override string ToString() => $"{Apellido}, {Nombre} ({Edad} {( EsHombre ? "H" : "M" )})";
		}

		static List<Persona> datos = new List<Persona> {
			new Persona{ Nombre = "Alejandro", Apellido = "Di Battista", Edad = 51, EsHombre = true  },
			new Persona{ Nombre = "Mirta",     Apellido = "Peroña",      Edad = 72, EsHombre = false },
			new Persona{ Nombre = "Franco",    Apellido = "Di Battista", Edad = 14, EsHombre = true  },
			new Persona{ Nombre = "Mirta",     Apellido = "Peroña",      Edad = 50, EsHombre = false },
			new Persona{ Nombre = "Maira",     Apellido = "Di Battista", Edad = 15, EsHombre = false },
		};

		interface IFiltro
		{
			bool Condicion( Persona p );
		}

		class Hombre : IFiltro
		{
			public bool Condicion( Persona p ) => p.EsHombre;
		}

		class Contiene : IFiltro
		{
			private string Texto;
			public Contiene( string texto ) => Texto = texto;
			public bool Condicion( Persona p ) => p.Apellido.Contains( Texto ) || p.Nombre.Contains( Texto );
		}

		class Invertir : IFiltro
		{
			private IFiltro Anterior;
			public Invertir( IFiltro anterior ) => Anterior = anterior;
			public bool Condicion( Persona p ) => !Anterior.Condicion( p );
		}

		class Y : IFiltro
		{
			private IFiltro A, B;
			public Y( IFiltro a, IFiltro b )
			{
				A = a;
				B = b;
			}
			public bool Condicion( Persona p ) => A.Condicion( p ) && B.Condicion( p );
		}
		class O : IFiltro
		{
			private IFiltro A, B;
			public O( IFiltro a, IFiltro b )
			{
				A = a;
				B = b;
			}
			public bool Condicion( Persona p ) => A.Condicion( p ) || B.Condicion( p );
		}

		class MenorA : IFiltro
		{
			int Edad;
			public MenorA( int edad ) => Edad = edad;
			public bool Condicion( Persona p ) => p.Edad < Edad;
		}

		class Menor : MenorA
		{
			public Menor() : base( 18 ) { }
			public bool Condicion( Persona p ) => p.Edad < 18;
		}

		class Mayor : IFiltro
		{
			IFiltro Base;
			public Mayor() => Base = new Invertir( new Menor() );
			public bool Condicion( Persona p ) => Base.Condicion( p );
		}
		static IEnumerable<Persona> Filtrar( this IEnumerable<Persona> personas, IFiltro filtro )
		{
			foreach( var persona in personas ) {
				if( filtro.Condicion( persona ) )
					yield return persona;
			}
		}
		static void Main( string[] args )
		{
			Console.WriteLine( "LISTADO DE PERSONAS" );
			IFiltro hombre = new Hombre();
			IFiltro mujer = new Invertir( hombre );

			IFiltro condicion = new O( new Y( hombre, new Menor() ), new Y( mujer, new Mayor() ) );

			//f = new Y( new Menor(), f );
			foreach( var persona in Filtrar( datos, condicion ) ) {
				Console.WriteLine( $"· {persona}" );
			}
			Console.ReadLine();
		}
	}
}
