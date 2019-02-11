using System;
using System.Collections.Generic;
using System.Text;

namespace Ejercicios13
{
	class Persona
	{
		public string Nombre { get; set; }
		public string Apellido { get; set; }

		public IDecorador Acciones { get; set; }
		public void Ejecutar()
		{
			Acciones?.Ejecutar( this );
		}
	}

	interface IDecorador
	{
		void Ejecutar( Persona p );
	}

	class Mostrar : IDecorador
	{
		public void Ejecutar( Persona p )
		{
			Console.WriteLine( $"MOSTRANDO > Persona: {p.Nombre} {p.Apellido}" );
		}
	}
	class Guardar : IDecorador
	{
		IDecorador anterior;
		public Guardar( IDecorador anterior )
		{
			this.anterior = anterior;
		}
		public void Ejecutar( Persona p )
		{
			anterior.Ejecutar( p );
			Console.WriteLine( $"GUARDANDO > Persona: {p.Nombre} {p.Apellido}" );
		}
	}

	class Depurar : IDecorador
	{
		public bool Depurando { get; set; }
		public Depurar( bool depurar )
		{
			this.Depurando = depurar;
		}
		public void Ejecutar( Persona p )
		{
			if( Depurando )
				Console.WriteLine( $"DEPURAR > Persona: {p.Nombre} {p.Apellido}" );
		}
	}
	class Componer : IDecorador
	{
		List<IDecorador> lista = new List<IDecorador>();
		public void Agregar( IDecorador otro ) => lista.Add( otro );

		public void Ejecutar( Persona p )
		{
			foreach( var d in lista ) {
				d.Ejecutar( p );
			};
		}
	}

	class Condicional : IDecorador
	{
		public IDecorador Anterior{ get; set; }
		public Predicate<Persona>Condicion { get; set; }
		public Condicional(IDecorador anterior, Predicate<Persona> condicion ) {
			Anterior  = anterior;
			Condicion = condicion;
		}
		public void Ejecutar(Persona p )
		{
			if( Condicion( p ) ) {
				Anterior.Ejecutar( p );
			}
		}
	}
	class CondicionalIF : IDecorador
	{
		public IDecorador Si { get; set; }
		public IDecorador No { get; set; }
		public Predicate<Persona> Condicion { get; set; }
		public CondicionalIF( IDecorador si, IDecorador no, Predicate<Persona> condicion )
		{
			Si = si;
			No = no;
			Condicion = condicion;
		}
		public void Ejecutar( Persona p )
		{
			if( Condicion( p ) ) {
				Si.Ejecutar( p );
			} else {
				No.Ejecutar( p );
			}
		}
	}
	class Decorador
	{
		static void Main( string[] args )
		{
			var p  = new Persona { Nombre = "Alejandro", Apellido = "Di Battista" };
			var p1 = new Persona { Nombre = "Alejandro", Apellido = "Di Battista" };

			var l = new Componer();
			l.Agregar( new Mostrar() );
			l.Agregar( new Depurar(true) );

			var cd = new CondicionalIF( 
				new Mostrar(), 
				new Depurar(true), 
				i => i.Nombre.Length > i.Apellido.Length );

			l.Agregar( cd );
			var d = new Guardar( l );
			var c = new Condicional( d, i => i.Nombre == "Alejandro" );

			p.Acciones = c;

			p.Ejecutar();



			Console.ReadLine();

		}
	}
}
