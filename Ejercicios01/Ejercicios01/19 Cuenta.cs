using System;
using System.Collections.Generic;
using System.Text;

namespace Ejercicios
{

	static class Programa
	{
		enum Tipo { Oro, Plata, Base }

		interface ICalculador
		{
			double CalcularPunto( double monto );
		}
		class Cuenta
		{
			public double Balance;
			public double Puntos;

			private ICalculador calculador;
			public Cuenta(ICalculador calculador)
			{
				this.calculador = calculador;
			}
			public void Registrar( double monto )
			{
				Balance += monto;
				if(! (calculador is null) )
					Puntos  += calculador.CalcularPunto( monto );
			}
		}

		class CalcularOro : ICalculador
		{
			public double CalcularPunto( double monto ) => monto / 5;
		}

		class CalcularPlata : ICalculador
		{
			public double CalcularPunto( double monto ) => monto / 10;
		}

		static Cuenta Crear( Tipo tipo )
		{
			switch( tipo ) {
				case Tipo.Oro:   return new Cuenta( new CalcularOro() );
				case Tipo.Plata: return new Cuenta( new CalcularPlata() );
				case Tipo.Base:  return new Cuenta( null );
			}
			return null;
		}

		static void Main( string[] args )
		{
			//var c = new Cuenta(Tipo.Oro);
			var c = Crear( Tipo.Oro );
			c.Registrar( 100 );
			c.Registrar( 200 );
			Console.WriteLine( $"B: {c.Balance} p:{c.Puntos}" );
			Console.ReadLine();

		}
	}
}
