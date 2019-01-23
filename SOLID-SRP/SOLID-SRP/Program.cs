using System;
using System.Collections.Generic;
using System.Text;

namespace Solid
{
	namespace SRP
	{
		class Persona
		{
			public string Nombre { get; set; }
			public string Apellido { get; set; }
		}


		class DemoSRP
		{
			static void Main( string[] args )
			{
				Console.WriteLine( "Alta de usuario en Amasin!" );

				Persona p = new Persona();

				Console.Write( "¿Cúal es el nombre? >" );
				p.Nombre = Console.ReadLine();

				Console.Write( "¿Cúal es el Apellido? >" );
				p.Apellido = Console.ReadLine();

				if( string.IsNullOrWhiteSpace( p.Nombre ) ) {
					Console.WriteLine( "Debe ingresar un Nombre valido" );
					Console.ReadLine();
					return;
				}

				if( string.IsNullOrWhiteSpace( p.Apellido ) ) {
					Console.WriteLine( "Debe ingresar un Nombre Apellido" );
					Console.ReadLine();
					return;
				}

				Console.WriteLine( $"El nombre de usuario es {p.Nombre.Substring( 0, 1 )}{p.Apellido.Replace(" ", "")}" );
				Console.ReadLine();
			}
		}
	}
}
