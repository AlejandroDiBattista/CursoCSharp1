using System;
using System.Collections.Generic;
using System.Linq;

namespace Ejercicios01
{
	class Program
	{
		interface ICalcularFrecuencia
		{
			void Registrar( string frase );
			int Frecuencia( string palabra );
			IEnumerable<(string palabra, int frecuencia)> Top( int cantidad );
			IEnumerable<string> Palabras();
		}

		class CalcularFrecuencia : ICalcularFrecuencia
		{
			Dictionary<string, int> contar = new Dictionary<string, int>();
			public int Frecuencia( string palabra ) => contar.ContainsKey( palabra ) ? contar[ palabra ] : 0;

			public IEnumerable<string> Palabras()
			{
				return contar.Keys;
			}

			public void Registrar( string frase )
			{
				foreach( var palabra in frase.Split( " " ).Where( i => i.Length > 3 ) )
				{
					if( !contar.ContainsKey( palabra ) ) contar[ palabra ] = 0;
					contar[ palabra ] += 1;
				}
			}

			public IEnumerable<(string palabra, int frecuencia)> Top( int cantidad )
			{
				return contar
							.Select( e => (palabra: e.Key, frecuencia: e.Value) )
							.OrderByDescending( e => e.frecuencia )
							.Take( cantidad );
			}

		}
		static void Main1( string[] args )
		{
			ICalcularFrecuencia c = new CalcularFrecuencia();
			c.Registrar( "el perro persigue al gato" );
			c.Registrar( "el gato escapa del perro" );
			c.Registrar( "el gato persigue un raton" );
			c.Registrar( "el perro es blanco" );
			c.Registrar( "el gato es negro" );
			c.Registrar( "el raton es cobarde" );
			c.Registrar( "el perro es valiente" );
			c.Registrar( "el gato es cobarde" );
			c.Registrar( "el raton no es verde" );

			Console.WriteLine( "PROBANDO Cálculo de Frecuencia" );
			Test( c.Palabras().Count(), 10, "Cantidad de palabras diferentes" );

			Test( c.Frecuencia( "el" ), 0, "Ignora las palabras cortas (<=3)" );
			Test( c.Frecuencia( "leon" ), 0, "Ignora las palabras que no esten registradas" );
			Test( c.Frecuencia( "gato" ), 5, "Hay 5 gatos" );
			Test( c.Frecuencia( "raton" ), 3, "Hay 3 ratones" );

			var m = c.Top( 3 );
			Test( m.First().palabra, "gato", "La mas frecuente es GATO" );
			Test( m.First().frecuencia, 5, "Hay 5 GATOS" );

			Test( m.Last().palabra, "raton", "La menos frecuente es RATON" );
			Test( m.Last().frecuencia, 3, "Hay 3 RATONES" );

			//foreach( var (p, f) in c.Top(3) )
			//	Console.WriteLine( $" · {p,-10} = {f}" );

			Console.ReadLine();
		}

		static void Test<T>( T real, T esperado, string descripcion )
		{
			if( real.Equals( esperado ) )
			{
				Console.WriteLine( $" + {descripcion} ok." );
			} else
			{
				Console.WriteLine();
				Console.WriteLine( $" - ERROR en {descripcion}" );
				Console.WriteLine( $"     Esperaba : {esperado}" );
				Console.WriteLine( $"     Recibido : {real}" );
				Console.WriteLine();
			}
		}
	}
}
