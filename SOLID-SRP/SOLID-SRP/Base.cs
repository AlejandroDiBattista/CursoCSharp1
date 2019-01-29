using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Solid
{
	namespace SRP
	{
		class Persona
		{
			public string Nombre { get; set; }
			public string Apellido { get; set; }
		}

		static class ValidarPersona
		{
			public static bool Valido(Persona p, out string error)
			{
				error = null;
				if( EsValido( p.Nombre ) ) {
					error = "Nombre";
					return false ;
				}

				if( EsValido( p.Apellido ) ) {
					error = "Apellido";
					return false ;
				}
				return true;

				bool EsValido(string valor ) => string.IsNullOrWhiteSpace( valor );
			}
		}
		static class CargarPersona
		{
			public static Persona Cargar(IEntrada entrada)
			{
				Persona p  = new Persona();

				Salida.PedirCampo( "Nombre" );
				p.Nombre   = entrada.Leer();

				Salida.PedirCampo( "Apellido" );
				p.Apellido = entrada.Leer();
				return p;
			}
		}

		static class GenerarUsuario
		{
			public static string Generar(Persona p )
			{
				var usuario = (p.Nombre.Substring( 0, 1 ) + p.Apellido.Replace( " ", "" )).ToLower();
				return usuario;
			}
		}

		interface IEntrada
		{
			string Leer();
		}
		class EntradaConsola : IEntrada
		{
			public  void Pausa() =>  Console.ReadKey();
			public  string Leer() => Console.ReadLine();
		}
		class EntradaSimulada : IEntrada
		{
			IList<string> datos = new List<string>();

			public string Leer()
			{
				var tmp = datos[ 0 ];
				datos.RemoveAt( 0 );
				return tmp;
			}

			public void Registrar(string entrada )
			{
				datos.Add( entrada );
			}

		}
		static class Salida
		{
			public static void Bienvenida() => Console.WriteLine( "Alta de usuario en AMAZIN!" );
			public static void Despedida()
			{
				Console.WriteLine("Presiona una tecla para terminar");
			}
			public static void MostrarError(string campo )
			{
				Console.WriteLine( $"Debe ingresar un {campo} válido" );
			}
			public static void MostrarUsuario(string usuario )
			{
				Console.WriteLine( $"El nombre de usuario es {usuario}" );
			}
			public static void PedirCampo(string campo )
			{
				Console.Write( $"¿Cúal es el {campo}? >" );
			}
		}
		class DemoSRP
		{
			static void Main( string[] args )
			{
				//var e = new EntradaConsola();

				var e = new EntradaSimulada();
				e.Registrar( "Alejandro" );
				e.Registrar( "Di Battista" );

				var pe = CargarPersona.Cargar( e );
				Debug.Assert( pe.Nombre == "Alejandro" );
				Debug.Assert( pe.Apellido == "Di Battista" );


				Salida.Bienvenida();

				var p = CargarPersona.Cargar(e);

				var error = "";
				if( ValidarPersona.Valido( p, out error ) ) {
					var usuario = GenerarUsuario.Generar( p );
					Salida.MostrarUsuario( usuario );
				} else {
					Salida.MostrarError( error );
				}

				Salida.Despedida();
				e.Pausa();

				var u = new Persona { Nombre = "Alejadro", Apellido = "Di Battista" };
				Debug.Assert( GenerarUsuario.Generar( u ) == "adibattista" );
			}
		}
	}
}
