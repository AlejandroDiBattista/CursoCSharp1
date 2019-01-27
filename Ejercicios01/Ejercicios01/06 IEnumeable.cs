using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ejercicios06
{
	static class DemoEnumerable
	{
		static TResult Agregador<TSource, TResult>( this IEnumerable<TSource> lista, TResult inicial, Func<TResult, TSource, TResult> acumulador )
		{
			TResult suma = inicial;
			foreach( var actual in lista )
				suma = acumulador( suma, actual );
			return suma;
		}

		static int Min( this IEnumerable<int> lista ) //Solo aplicables a 'int' porque no esta definido "<"
		{
			return lista.Aggregate( ( min, a ) => a < min ? a : min );
		}

		static IEnumerable<T> Concatenar<T>( this IEnumerable<T> a, IEnumerable<T> b )
		{
			foreach( var x in a ) yield return x;
			foreach( var x in b ) yield return x;
		}

		static IEnumerable<T> Agregar<T>( this IEnumerable<T> a, T elemento )
		{
			foreach( var x in a ) yield return x;
			yield return elemento;
		}
		static IEnumerable<T> Distinto<T>( this IEnumerable<T> a )
		{
			var excluir = new HashSet<T>();
			foreach( var x in a ) {
				if( excluir.Add( x ) )
					yield return x;
			}
		}
		static IEnumerable<int> M2()
		{
			return Enumerable.Range( 1, 6 ).Select( i => i * 2 );
		}
		static IEnumerable<int> M3()
		{
			for( var i = 3; i <= 18; i += 3 )
				yield return i;
		}
		static string Convertir<T>( T valor )
		{
			if( valor is IGrouping<int, int> grupo ) {
				return $"[ { grupo.Key } : { string.Join( ", ", grupo.Select( Convertir ) ) } ]";
			} else
				return $"{ valor, 3 }";
		}
		static void Titulo( string titulo )
		{
			Console.WriteLine( $"\n {titulo}" );
		}
		static void Mostrar<T>( this IEnumerable<T> lista, string mensaje = "" )
		{
			Console.WriteLine( $" · {mensaje,-20} -> [ {string.Join( ", ", lista.Select( Convertir ) ) } ]" );
		}
		static void Mostrar<T>( string mensaje, T valor )
		{
			Console.WriteLine( $" · {mensaje,-20} {Convertir( valor )}" );
		}

		static void Main( string[] args )
		{
			Console.WriteLine( "* DEMO Enumerable *\n" );
			IEnumerable<int> m2 = new int[] { 2, 4, 6, 8, 10, 12 };  // M2();
			IEnumerable<int> m3 = new int[] { 3, 6, 9, 12, 15, 18 }; // M3();
			IEnumerable<int> m23 = m2.Concat( m3 );

			Titulo( "FUENTE DE DATOS" );
			m2.Mostrar( "m2" );
			M2().Mostrar( "M2" );
			m3.Mostrar( "m3" );
			M3().Mostrar( "M3" );
			Console.WriteLine();

			Titulo( "GENERA DATOS" );
			Enumerable.Range( 1, 10 ).Mostrar( "Range 1..10" );
			Enumerable.Repeat( 10, 5 ).Mostrar( "Repeat 10 x 5" );

			Titulo( "TRANSFORMA" );
			m2.Select( i => i * i ).Mostrar( "Select i*i" );

			Titulo( "OPERACIONES CONJUNTO" );
			m2.Intersect( m3 ).Mostrar( "Intersect m2*m3" );
			m2.Union( m3 ).Mostrar( "Union m2+m3" );
			m2.Except( m3 ).Mostrar( "Except m2-m3" );
			m3.Except( m2 ).Mostrar( "Except m3-m2" );

			Titulo( "FUNCIONES AGREGAR ELEMENTOS" );
			m2.Concat( m3 ).Mostrar( "Concat m2+m3" );
			m2.Append( 100 ).Mostrar( "Append M2+100" );
			m2.Prepend( 100 ).Mostrar( "Prepend 100" );

			Titulo( "ORDENA" );

			m2.OrderBy( i => i % 3 ).Mostrar( "OrderBy %3" );
			m2.OrderBy( i => i % 3 ).ThenByDescending( i => i ).Mostrar( "ThenBy" );
			m2.OrderByDescending( i => i ).Mostrar( "OrderByDescending" );
			m2.Reverse().Mostrar( "Reverse" );

			Titulo( "SACA CONJUNTO" );
			m2.Skip( 2 ).Mostrar( "Skip 2" );
			m2.SkipLast( 2 ).Mostrar( "SkipLast 2" );
			m2.Take( 2 ).Mostrar( "Take 2" );
			m2.TakeLast( 2 ).Mostrar( "TakeLast 2" );

			Titulo( "FILTRAR DATOS" );
			m2.Where( i => i < 10 ).Mostrar( "Where < 10" );

			m2.SkipWhile( i => i < 7 ).Mostrar( "SkipWhile < 7" );
			m2.TakeWhile( i => i < 7 ).Mostrar( "TakeWhile < 7" );
			m23.Distinct().Mostrar( "Distint (m23)" );

			Titulo( "AGRUPAR DATOS" );
			m2.GroupBy( i => i % 3 ).Mostrar( "GroupBy m2 % 3" );
			m2.Zip( m3, ( a, b ) => $"({a}, {b})" ).Mostrar( "Zip" );

			Titulo( "CALCULAR VALORES" );
			Mostrar( "Aggregate s+i", m2.Aggregate( ( acumulador, elemento ) => acumulador + elemento ) );
			Mostrar( "Aggregate s+i.lenght", "uno dos tres cuatro cinco".Split( " " ).Aggregate( 0, ( s, i ) => s + i.Length ) );
			Mostrar( "Sum ", m2.Sum() );
			Mostrar( "Count", m2.Count() );
			Mostrar( "Count <7", m2.Count( i => i < 7 ) );
			Mostrar( "Averange", m2.Average() );
			Mostrar( "All >1", m2.All( i => i > 1 ) );
			Mostrar( "Any >10", m2.Any( i => i > 10 ) );
			Mostrar( "Contains 6", m2.Contains( 6 ) );
			Mostrar( "Min", m2.Min() );
			Mostrar( "Max", m2.Max() );

			Console.ReadLine();
		}
	}
}
