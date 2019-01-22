using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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

	interface IProducto
	{
		int Codigo { get; }
		string Descripcion { get; }
		double Precio { get; }
		int Cantidad { get; set; }
	}

	class Producto : IProducto
	{
		public int Codigo { get; set; }
		public string Descripcion { get; set; }
		public double Precio { get; set; }
		public int Cantidad { get; set; }
		public Producto(int codigo, string descripcion, double precio, int cantidad )
		{
			this.Codigo = codigo;
			this.Descripcion = descripcion;
			this.Precio = precio;
			this.Cantidad = cantidad;
		}
	}
	class Inventario : IInventario
	{
		Dictionary<int, IProducto> datos = new Dictionary<int, IProducto>();
		public int CantidadProductos => datos.Count();

		public int CantidadUnidades => datos.Sum( p => p.Value.Cantidad ); // datos.Select( p => p.Value.Cantidad ).Sum();

		public double Precio( int codigo ) => datos.ContainsKey(codigo) ? datos[codigo].Precio : 0;

		public void Registrar( int codigo, string descripcion, double precio, int cantidad )
		{
			if( !datos.ContainsKey( codigo ) )
				datos[ codigo ] = new Producto(codigo, descripcion, precio, 0);
			
			datos[ codigo ].Cantidad += cantidad;
		}

		public bool Vender( int codigo, int cantidad )
		{
			if( !datos.ContainsKey( codigo ) )        return false;
			if( datos[ codigo ].Cantidad < cantidad ) return false;

			datos[ codigo ].Cantidad -= cantidad;
			return true;
		}
	}

	class Factura : IFactura
	{
		Dictionary<int, int> items = new Dictionary<int, int>();
		IInventario i;

		public Factura( IInventario i ) => this.i = i;
		public int CantidadProductos => items.Count;

		public int Unidades => items.Values.Sum();

		public double ImporteTotal => items.Select( p => p.Value * i.Precio( p.Key ) ).Sum(); 

		public void Vender( int codigo, int cantidad )
		{
			if( i.Vender( codigo, cantidad )) {
				if( !items.ContainsKey( codigo ) ) items[ codigo ] = 0;
				items[ codigo ] += cantidad;
			};
		}
	}
	class DemoFacturacion
	{
		static void Main( string[] args )
		{
			IInventario i = new Inventario();
			i.Registrar( 1, "iPhone Xr",     800, 5 );
			i.Registrar( 2, "iPad Pro",     1200, 2 );
			i.Registrar( 3, "Mac Book Pro", 2000, 2 );

			Test( i.Precio( 1 ),       800, "Consulta el precio" );
			Test( i.CantidadProductos,   3, "Consulta la cantidad de productos" );
			Test( i.CantidadUnidades,    9, "Consulta la cantidad de unidades" );

			IFactura f = new Factura(i);
			f.Vender( 1, 2 );
			f.Vender( 3, 1 );

			Test( f.CantidadProductos, 2, "Consulta cantidad de productos" );
			Test( i.CantidadUnidades,  6, "Descuenta 3 unidades" );

			f.Vender( 3, 1 );
			Test( f.CantidadProductos, 2, "No agrega nuevos productos si ya existia" );
			Test( i.CantidadUnidades,  5, "Descuenta 1 unidades mas" );

			f.Vender( 1, 10 );
			Test( f.CantidadProductos, 2, "No vende productos que no tenga existencia" );
			Test( i.CantidadUnidades,  5, "No descuenta nada" );

			Test( f.Unidades, 4, "Unidades totales" );
			Test( f.CantidadProductos,    2, "Cantidad de productos" );
			Test( f.ImporteTotal,      5600, "Total general" );

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
