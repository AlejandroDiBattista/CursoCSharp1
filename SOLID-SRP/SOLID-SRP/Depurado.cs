using System;
using System.Collections.Generic;
using System.Text;

namespace Solid
{
	namespace DemoSRP
	{
		class Persona
		{
			public string Nombre { get; set; }
			public string Apellido { get; set; }
		}

		class Mensajes
		{
			public static void Bienvenida() => Console.WriteLine( "Alta de usuarios de AMAZIN!" );
			public static void Finalizar()
			{
				Console.Write( "Presiona <ENTER> para finalizar" );
				Console.ReadLine();
			}

			public static void MostrarError( string campo ) => Console.WriteLine( $"Debe ingresar un { campo } valido" );
			public static void MostrarUsuario( string usuario ) => Console.WriteLine( $"El nombre de usuario es {usuario}" );
			public static string PedirDato( string campo )
			{
				Console.Write( $"¿Cúal es el {campo}? > " );
				return Console.ReadLine();
			}
		}

		class LeerPersona
		{
			public static Persona Leer()
			{
				Persona persona = new Persona();

				persona.Nombre = Mensajes.PedirDato( "Nombre" );
				persona.Apellido = Mensajes.PedirDato( "Apellido" );

				return persona;
			}
		}

		class GenerarUsuario
		{
			public static void Generar( Persona usuario )
			{
				var usuarioId = usuario.Nombre.Substring( 0, 1 ) + usuario.Apellido.Replace( " ", "" );
				Mensajes.MostrarUsuario( usuarioId );
			}

		}
		class ValidarPersona
		{
			public static bool Valida( Persona persona )
			{
				if( string.IsNullOrWhiteSpace( persona.Nombre ) ) {
					Mensajes.MostrarError( "Nombre" );
					return false;
				}

				if( string.IsNullOrWhiteSpace( persona.Apellido ) ) {
					Mensajes.MostrarError( "Apellido" );
					return false;
				}
				return true;
			}

		}
		class DemoSRP
		{
			static void Main( string[] args )
			{
				Mensajes.Bienvenida();

				var p = LeerPersona.Leer();

				if( ValidarPersona.Valida( p ) ) {
					GenerarUsuario.Generar( p );
				}

				Mensajes.Finalizar();
			}
		}
	}
}
