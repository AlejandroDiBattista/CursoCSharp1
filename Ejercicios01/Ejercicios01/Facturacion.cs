using System;
using System.Collections.Generic;
using System.Text;

namespace Ejercicios02
{
	interface IInventario
	{
		void Registrar( int codigo, string descripcion, double precio, int cantidad );
		bool Vender( int codigo, int cantidad );
		double Precio( int codigo );
		int CantidadProductos { get; }
		int CantidadUnidades { get; }
	}

	interface IFactura
	{
		void Vender( int codigo, int cantidad );
		int CantidadProductos { get; }
		int Unidades { get; }
		double ImporteTotal { get; }
	}
	
	class DemoFacturacion
	{
		static void Main( string[] args )
		{
			IInventario i;
			i.Registrar( 1, "iPhone Xr",     800, 5 );
			i.Registrar( 2, "iPad Pro",     1200, 2 );
			i.Registrar( 3, "Mac Book Pro", 2000, 2 );

			Test( i.Precio( 1 ),       800, "Consulta el precio" );
			Test( i.CantidadProductos,   3, "Consulta la cantidad de productos" );
			Test( i.CantidadUnidades,    9, "Consulta la cantidad de unidades" );

			IFactura f;
			f.Vender( 1, 2 );
			f.Vender( 3, 2 );

			Test( f.CantidadProductos, 2, "Consulta cantidad de productos" );
			Test( i.CantidadUnidades,  7, "Descuenta 2 unidades" );

			f.Vender( 3, 1 );
			Test( f.CantidadProductos, 2, "No agrega nuevos productos si ya existia" );
			Test( i.CantidadUnidades,  6, "Descuenta 1 unidades mas" );

			f.Vender( 1, 10 );
			Test( f.CantidadProductos, 2, "No vende productos que no tenga existencia" );
			Test( i.CantidadUnidades,  6, "No descuenta nada" );

			Test( f.Unidades, 5, "Unidades totales" );
			Test( f.CantidadProductos,    2, "Cantidad de productos" );
			Test( f.ImporteTotal,      5200, "Total general" );

			Console.ReadLine();
		}

		static void Test<T>( T real, T esperado, string descripcion )
		{
			if( real.Equals( esperado ) )
			{
				Console.WriteLine( $" + {descripcion} ok." );
			} else
			{
				Console.WriteLine();
				Console.WriteLine( $" - ERROR en {descripcion}" );
				Console.WriteLine( $"     Esperaba : {esperado}" );
				Console.WriteLine( $"     Recibido : {real}" );
				Console.WriteLine();
			}
		}
	}
}
