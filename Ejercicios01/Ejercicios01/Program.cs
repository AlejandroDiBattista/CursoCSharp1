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
			IEnumerable<(string palabra, int frecuencia)> Top(int cantidad); 
		}

		
		static void Main( string[] args )
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

			Console.WriteLine("PROBANDO Cálculo de Frecuencia");

			Test( c.Frecuencia( "el" ), 0, "Ignora las palabras cortas (<=3)" );
			Test( c.Frecuencia( "del" ), 0, "Ignora las palabras cortas (<=3)" );
			Test( c.Frecuencia( "gato" ), 4, "Hay 5 gatos" );
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
