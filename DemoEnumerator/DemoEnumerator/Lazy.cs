using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoEnumerator
{
	static class Lazy
	{
		delegate bool Condicion( int valor );
		interface IEnumerador
		{
			bool Avanzar();
			int Actual { get; }
		}

		class Numeros : IEnumerador
		{
			int maximo;
			int numero = 0;
			public Numeros( int maximo )
			{
				this.maximo = maximo;
			}
			public int Actual => numero;
			public bool Avanzar()
			{
				numero += 1;
				//Console.WriteLine( $" · Numero: {numero}" );
				return numero <= maximo;
			}
		}

		class Pares : IEnumerador
		{
			IEnumerador numero;
			public Pares( IEnumerador numero )
			{
				this.numero = numero;
			}
			public int Actual => numero.Actual;
			public bool Avanzar()
			{
				while( numero.Avanzar() )
				{
					//Console.WriteLine( $" ·· Pares: {numero.Actual}" );
					if( numero.Actual % 2 == 0 )
						return true;
				}
				return false;
			}
		}
		class Filtrar : IEnumerador
		{
			IEnumerador numero;
			Condicion condicion;
			public Filtrar( IEnumerador numero, Condicion condicion )
			{
				this.numero = numero;
				this.condicion = condicion;
			}
			public int Actual => numero.Actual;
			public bool Avanzar()
			{
				while( numero.Avanzar() )
				{
					//Console.WriteLine( $" ··· Filtrar: {numero.Actual}" );
					if( condicion( numero.Actual ) )
						return true;
				}
				return false;
			}
		}

		class EnumerarLista : IEnumerador
		{
			List<int> lista;
			int posicion = -1;
			public EnumerarLista( List<int> lista )
			{
				this.lista = lista;
			}
			public int Actual => lista[ posicion ];
			public bool Avanzar() => ++posicion < lista.Count;
		}
		static IEnumerable<int> NumerosY( this int maximo )
		{
			for( var numero = 0; ++numero <= maximo; )
				yield return numero;
		}
		static IEnumerable<int> ParesY( this IEnumerable<int> numeros )
		{
			foreach( var numero in numeros )
				if( numero % 2 == 0 )
					yield return numero;
		}
		static IEnumerable<int> FiltrarY( this IEnumerable<int> numeros, Condicion condicion )
		{
			foreach( var numero in numeros )
				if( condicion( numero ) )
					yield return numero;
		}

		static IEnumerador NumerosE( this int maximo ) => new Numeros( maximo );
		static IEnumerador ParesE( this IEnumerador numeros ) => new Pares( numeros );
		static IEnumerador FiltrarE( this IEnumerador numeros, Condicion condicion ) => new Filtrar( numeros, condicion );
		delegate void Accion( int v );

		static void Mostrar(this IEnumerador origen,string descripcion)
		{
			Console.WriteLine($" MOSTRAR: Creación de instancias > {descripcion}");
			while( origen.Avanzar() )
				Console.WriteLine($" · {origen.Actual} ");
			Console.WriteLine(".\n");
		}
		static void FormasDeConstruirEnumeradoresEncadenados( )
		{
			Console.WriteLine("DEMOSTRACIÓN Diferentes maneras de construir un enumerador encadenado\n");

			IEnumerador m1 = new Filtrar( new Pares( new Numeros( 10 ) ), i => i < 7 );
			Mostrar( m1, "INSTANCIAS ANIDADAS" );


			IEnumerador m2 = FiltrarE( ParesE( NumerosE( 10 ) ), i => i < 7 );
			Mostrar( m2, "FUNCIONES ANIDADAS" );


			IEnumerador m3 = new Numeros( 10 );
			m3 = new Pares( m3 );
			m3 = new Filtrar( m3, i => i < 7 );
			Mostrar( m3, "SECUENCIAL" );


			IEnumerador m4 = NumerosE( 10 );
			m4 = ParesE( m4 );
			m4 = FiltrarE( m4, i => i < 7 );
			Mostrar( m4, "SECUENCIALES con FUNCIÓN AUXILIAR" );


			IEnumerador m5 = NumerosE(10) // Tambien se puede escribir como "10.NumerosE()"
							   .ParesE()
							   .FiltrarE( i => i < 7 );
			Mostrar( m5, "ENCADENADA con EXTENCIÓN DE MÉTODOS" );


			Console.ReadLine();
		}
		static void ComparacionEntreEnumeradorVSYieldReturn( )
		{
			Console.WriteLine( "DEMOSTRACIÓN Ejecución LAZY" );


			Console.WriteLine( "\n1º Números Pares Menores a 5 (Enumerador)" );
			//var re = FiltrarE( ParesE( NumerosE( 10 ) ), i => i < 5 );
			var re =  NumerosE(10).ParesE().FiltrarE( i => i < 5 );
			while( re.Avanzar() )
				Console.WriteLine( $" - {re.Actual}" );


			Console.WriteLine( "\n2º Números Pares Menores a 5 (yield return)" );
			//var ryb = FiltrarY( ParesY( NumerosY( 10 ) ), i => i < 5 );
			var ryb = NumerosY(10).ParesY().FiltrarY( i => i < 5 );
			foreach( var current in ryb )
				Console.WriteLine( $" - {current}" );


			Console.WriteLine( "\n3º Números Pares Menores a 5 (linQ)" );
			var rlq = Enumerable.Range( 1, 10 ).Where( i => i % 2 == 0 ).Where( i => i < 7 );
			foreach( var current in rlq )
				Console.WriteLine( $" - {current}" );


			Console.ReadLine();
		}

		static void Main( string[] args )
		{
			FormasDeConstruirEnumeradoresEncadenados();
			ComparacionEntreEnumeradorVSYieldReturn();
		}
	}
}