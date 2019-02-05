using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ejercicios09
{
	static class DemoFuncionHash
	{
		public static byte CalcularSuma( string texto )
		{
			return (byte)texto.Sum( i => (int)i );
			//int total = 0;
			//foreach( var item in texto.ToCharArray() ) {
			//	total += (int)item;
			//}	
			//return (byte)total;
		}
		public static byte CalcularXor( string texto )
		{
			return (byte)texto.Aggregate( 0, ( total, item ) => total ^ (int)item );
			//int total = 0;
			//foreach( var item in texto.ToCharArray() ) {
			//	total = total ^ (int)item;
			//}
			//return (byte)total;
		}

		public static byte CalcularSecuencia( string texto )
		{
			return (byte)Enumerable.Range( 1, texto.Length ).Zip( texto, ( i, item ) => i * (int)item ).Sum();
			//int total = 0;
			//int i = 1;
			//foreach( var item in texto.ToCharArray() ) {
			//	total +=  i * (int)item;
			//	i += 1;
			//}
			//return (byte)total;
		}
		public static byte CalcularPrimo( string texto )
		{
			return (byte)texto.Aggregate( texto.Length, ( t, i ) => t * 37 + (int)i );
			//int total = texto.Length;
			//foreach( var item in texto.ToCharArray() ) {
			//	total = total * 37 + (int)item ;
			//}
			//return (byte)total;
		}

		public static byte CalcularComplejo( string texto )
		{
			var h = new HashCode();
			foreach( var item in texto.ToCharArray() ) {
				h.Add(item);
			}
			return (byte)h.ToHashCode();
		}

		static void Mostrar(string descripcion, Func<string,byte> hash )
		{
			var prueba = new[] { "ale", "jan", "dro", "lea" };

			Console.WriteLine( $"DEMO {descripcion}" );
			foreach( var palabra in prueba ) {
				Console.WriteLine( $" · Hash({palabra})={hash( palabra )}" );
			}
			Console.WriteLine();
		}

		static void Main( string[] args )
		{
			Console.WriteLine("* DEMO Funcion Hash*");
			//Mostrar( "Simple",	  CalcularSuma );
			//Mostrar( "Xor", CalcularXor );
			//Mostrar( "Secuencia", CalcularSecuencia );
			//Mostrar( "Primo", CalcularPrimo );
			Mostrar( "Complejo", CalcularComplejo );

			Console.ReadLine();
		}
	}
}
