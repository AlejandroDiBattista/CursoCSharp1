﻿using System;
using System.Collections.Generic;
using System.Linq;
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
			public bool Condicion( Persona p )
			{
				Console.WriteLine( $"En Condicion Hombre : {p}" );
				return p.EsHombre;
			}
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
			public virtual bool Condicion( Persona p ) => p.Edad < Edad;
		}

		class Menor : MenorA
		{
			public Menor() : base( 18 ) { }
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
		static IEnumerable<Persona> Filtrar( this IEnumerable<Persona> personas, Condicion condicion )
		{
			foreach( var persona in personas ) {
				if( condicion( persona ) )
					yield return persona;
			}
		}

		delegate bool Condicion( Persona a );

		static Condicion Not( Condicion condicion ) => p => !condicion( p );
		static Condicion Or( params Condicion[] condiciones ) => p => condiciones.Any( c => c( p ) );
		static Condicion And( params Condicion[] condiciones ) => p => condiciones.All( c => c( p ) );
		static Condicion EsMenor( int edad ) => p => p.Edad < edad;
		static Condicion Menores => p => EsMenor( 18 )( p );
		static Condicion EsMayor( int edad ) => p => p.Edad >= edad;
		static Condicion Mayores => p => EsMayor( 18 )( p );

		static Condicion EsHombre => p => p.EsHombre;
		static Condicion EsMujer => p => !EsHombre( p );
		static Condicion Contienen( string texto ) => p => p.Apellido.Contains( texto ) || p.Nombre.Contains( texto );

		static void Main( string[] args )
		{
			Console.WriteLine( "LISTADO DE PERSONAS" );

			foreach( var persona in datos.Filtrar( Or( And( EsHombre, EsMenor( 18 )), And( EsMujer, EsMayor( 25 ) ) ) ) )  {
				Console.WriteLine( $" · {persona}" );
			}

			Console.WriteLine( typeof( IFiltro ) );
			Console.ReadLine();
		}
	}
}
