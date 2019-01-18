using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace DemoTraductor
{
	class Traductor
	{
		Dictionary<string, string> sinonimos = new Dictionary<string, string>();
		public void Definir( string español, string ingles ) => sinonimos.Add( español, ingles );
		IEnumerable<string> Palabras( string frase ) => frase.Split( " " );
		public string Traducir( string frase ) =>
			string.Join( " ", Palabras(frase).Select( e => sinonimos.ContainsKey( e ) ? sinonimos[ e ] : "-" ) );

		public bool EsEspañol( string frase ) => Palabras( frase ).All( i => sinonimos.ContainsKey( i ) );
		public bool EsIngles( string frase ) => Palabras( frase ).All( i => sinonimos.ContainsValue( i ) );
	}

	class Demo
	{
		static void Test<T>(T real, T esperado, string descripcion )
		{
			if( !real.Equals( esperado ) )
			{
				Console.WriteLine();
				Console.WriteLine($"ERROR: {descripcion}");
				Console.WriteLine($"  Esperaba > {esperado}");
				Console.WriteLine($"  Recibió  > {real}");
				Console.WriteLine();
				Debug.Assert( false );
			} else
			{
				Console.WriteLine($"{descripcion} ok.");
			}
		}
		static void Main( string[] args )
		{
			var t = new Traductor();
			t.Definir( "cero",  "zero" );
			t.Definir( "uno",	"one" );
			t.Definir( "dos",	"two" );
			t.Definir( "tres",	"three" );
			t.Definir( "cuatro","four" );
			t.Definir( "cinco", "five" );
			t.Definir( "seis",	"six" );
			t.Definir( "siete", "seven" );
			t.Definir( "ocho",  "eigth" );
			t.Definir( "nueve", "nine" );

			Test( t.EsEspañol( "dos dos tres" ), true,				"¿Es texto en español?" );
			Test( t.EsEspañol( "dos two tres" ), false,				"¿NO Es texto en español?" );
			Test( t.EsIngles( "one two four" ),  true,				"¿Es texto en Ingles?" );
			Test( t.EsIngles( "uno two four" ),  false,				"¿NO Es texto en Ingles?" );
			Test( t.Traducir( "uno dos tres" ), "one two three",	"Traducción Español => Ingles" );
			Test( t.Traducir( "two two three" ), "dos dos tres",	"Traduccion Ingles => Español" );

			Console.WriteLine("Todo los test superados");
			Console.ReadLine();
		}
	}
}
