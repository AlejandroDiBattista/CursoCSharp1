using System;
using System.Collections.Generic;
using System.Text;

namespace Ejercicios12
{
	class Persona
	{
		public string Nombre { get; set; }
		public string Apellido { get; set; }
	}

	interface IDecorador
	{
		void Ejecutar( Persona p );
	}

	class Mostrar : IDecorador
	{
		public void Ejecutar( Persona p )
		{
			Console.WriteLine($"MOSTRANDO > Persona: {p.Nombre} {p.Apellido}") ;
		}
	}
	class Guardar : IDecorador
	{
		public void Ejecutar( Persona p )
		{
			Console.WriteLine( $"GUARDANDO > Persona: {p.Nombre} {p.Apellido}" );
		}
	}


	class Decorador
	{

		static void Main( string[] args )
		{
			var p = new Persona { Nombre = "Alejandro", Apellido = "Di Battista" };

			var m = new Mostrar(  );
			m.Ejecutar( p );
			
			Console.ReadLine();

		}
	}
}
