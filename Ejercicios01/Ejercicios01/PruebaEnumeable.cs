using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ejercicios03
{
	static class PruebaEnumerable
	{
		static TResult Agregador<TSource, TResult>( this IEnumerable<TSource> lista, TResult inicial, Func<TResult, TSource, TResult> acumulador )
		{
			TResult suma = inicial;
			foreach( var actual in lista ) {
				suma = acumulador( suma, actual );
			}
			return suma;
		}

		static int Sumar( this IEnumerable<int> lista)
		{
			return lista.Agregador( 0, ( s, a ) => s + a);
		}

		static int Contar<T>( this IEnumerable<T> lista )
		{
			return lista.Agregador( 0, ( s, a ) => s + 1 );
		}

		static bool Todos<T>(this IEnumerable<T> lista, Predicate<T> condicion )
		{
			return lista.Agregador( true, ( s, a ) => s && condicion(a) );
		}

		static bool Alguno<T>( this IEnumerable<T> lista, Predicate<T> condicion )
		{
			return lista.Agregador( false, ( s, a ) => s || condicion( a ) );
		}

		static IEnumerable<T> Distinto<T>( this IEnumerable<T> a )
		{
			var salida = new List<T>();
			foreach( var item in a ) {
				if( !salida.Contains( item )){
					salida.Add( item );
					yield return item;
				}
			}

			//var excluir = new HashSet<T>();
			//foreach( var x in a ) {
			//	if( !excluir.Add(x)) {
			//		yield return x;
			//	}
			//}

			//foreach(var x in new HashSet<T>( a ))
			//	yield return x;
		}

		static IEnumerable<T> Concatenar<T>(this IEnumerable<T> a, IEnumerable<T> b )
		{
			foreach( var x in a ) {
				yield return x;
			}
			foreach( var x in b ) {
				yield return x;
			}
		}
		static void Main( string[] args )
		{
			var m2 = new int[] { 2, 4, 6,  8, 10, 12 };
			var m3 = new int[] { 3, 6, 9, 12, 15, 18 };

			foreach( var item in m2.Concat(m3).Distinct() ) {
				Console.WriteLine(item);
			}
			//m2.Concat( m3 ).Distinct().ToList().ForEach(Console.WriteLine  );

			Console.ReadLine();
		}
	}
}
