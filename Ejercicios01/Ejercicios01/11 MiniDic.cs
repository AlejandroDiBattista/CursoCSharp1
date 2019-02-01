using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ejercicios11
{
	interface IDic
	{
		bool Contiene( string clave );
		void Agregar( string clave, int valor );
		int? Recuperar( string clave );
	}
	class DicLista: IDic
	{
		class Par
		{
			public string Clave;
			public int Valor;
		}

		List<Par> lista = new List<Par>();
		public bool Contiene( string clave )
		{
			return lista.Any( i => i.Clave == clave );
		}
		public void Agregar( string clave, int valor )
		{
			if( Contiene( clave ) ) return;
			lista.Add( new Par { Clave = clave, Valor = valor } );
		}

		public int? Recuperar( string clave )
		{
			if( !Contiene( clave ) ) return null;
			return lista.Find( i => i.Clave == clave ).Valor;
		}
	}

	class DicHash : IDic
	{
		class Par
		{
			public string Clave;
			public int Valor;
		}

		const int max = 10;
		Par[] lista = new Par[ max ];
		public bool Contiene( string clave )
		{
			int j = Hash( clave );
			int i = j;
			do {
				if( lista[ i ] == null ) break;
				if( lista[ i ].Clave == clave ) return true;
				i = ++i % max;
			} while( j != i );
			return false;
		}
		public void Agregar( string clave, int valor )
		{
			var j = Hash( clave );
			var i = j;
			do {
				if( lista[ i ] == null ) {
					lista[ i ] = new Par { Clave = clave, Valor = valor };
					break;
				};
				i = ++i % max;
			} while( j != i );
			return;
		}

		public int? Recuperar( string clave )
		{
			int j = Hash( clave );
			int i = j;
			do {
				if( lista[ i ] == null ) break;
				if( lista[ i ].Clave == clave ) return lista[ i ].Valor;
				i = ++i % max;
			} while( j != i );
			return null;
		}
		int Hash( string texto ) => texto.Aggregate( 0, ( h, i ) => h * 37 + (int)i ) % max;
	}

	class DicHashList : IDic
	{
		class Par
		{
			public string Clave;
			public int Valor;
		}

		const int max = 10;
		List<Par>[] lista = new List<Par>[ max ];
		public bool Contiene( string clave )
		{
			var i = Hash( clave );
			return lista[ i ] != null && lista[ i ].Any( j => j.Clave == clave );
		}
		public void Agregar( string clave, int valor )
		{
			if( Contiene( clave ) ) return;

			var i = Hash( clave );
			lista[ i ] = lista[ i ] ?? new List<Par>();
			lista[ i ].Add( new Par { Clave = clave, Valor = valor } );
		}

		public int? Recuperar( string clave )
		{
			if( !Contiene( clave ) ) return null;

			var i = Hash( clave );
			return lista[ i ].Find( j => j.Clave == clave ).Valor;
		}

		public int? this[string clave ]
		{
			get => Recuperar( clave );
			set => Agregar( clave, value ?? 0 );
		}

		int Hash( string texto ) => texto.Aggregate( 0, ( h, i ) => h * 37 + (int)i ) % max;
	}

	class DemoMiniDic
	{
		static void Mostrar(IDic d )
		{
			Console.WriteLine($"DEMO {d.GetType().Name}");
			d.Agregar( "uno", 1 );
			d.Agregar( "dos", 2 );
			Console.WriteLine( $" · Contiene(uno)  => {d.Contiene( "uno" )}" );
			Console.WriteLine( $" · Contiene(tres) => {d.Contiene( "tres" )}" );
			Console.WriteLine( $" · Recuperar(dos) => {d.Recuperar( "dos" )}" );
			Console.WriteLine();
		}

		static void Main( string[] args )
		{
			Console.WriteLine("*DEMO Dicionario");
			Mostrar( new DicLista() );
			Mostrar( new DicHash() );
			Mostrar( new DicHashList() );

			Console.WriteLine("DEMO []");
			var d = new DicHashList();
			d[ "uno" ] = 1;
			d[ "dos" ] = 2;
			Console.WriteLine( $" · Recuperar(dos) => {d.Recuperar( "dos" )}" );
			Console.WriteLine( $" · [dos] => {d["dos"]}" );
			Console.ReadLine();
		}
	}
}
