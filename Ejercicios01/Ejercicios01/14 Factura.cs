using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;
using static System.Math;

namespace Ejercicios14
{
	public static class Consola
	{
		public static void Pausa( string texto = "Pulsar <ENTER> para continuar..." )
		{
			Mostrar( texto, ConsoleColor.Red, true );
			while( Console.ReadKey().Key != ConsoleKey.Enter ) { }
		}
		public static void Titulo( string titulo, int ancho = 40 )
		{
			if( titulo.Length > 0 ) titulo = $"-| {titulo} |-";
			if( titulo.Length < ancho ) titulo = titulo + new string( '-', 40 - titulo.Length );

			Mostrar( titulo, ConsoleColor.Yellow );
			Console.WriteLine();
			Destacar = true;
		}
		public static void Mostrar( string texto, bool continuar = false )
		{
			if( continuar )
				Console.Write( texto );
			else
				Console.WriteLine( texto );
		}
		public static void Mostrar( string texto, ConsoleColor color, bool continuar = false )
		{
			var tmp = Color;
			Color = color;
			Mostrar( texto, continuar );
			Color = tmp;
		}

		public static string Leer( string texto = "" )
		{
			Console.Write( texto );
			return Console.ReadLine();
		}
		public static void Separar()
		{
			AlterarColor();
			Console.WriteLine();
		}

		public static ConsoleColor Color
		{
			get => Console.ForegroundColor;
			set => Console.ForegroundColor = value;
		}

		public static bool Destacar
		{
			get => Console.ForegroundColor == ConsoleColor.White;
			set => Console.ForegroundColor = value ? ConsoleColor.White : ConsoleColor.DarkGray;
		}
		static void AlterarColor()
		{
			Destacar = !Destacar;
		}
	}

	partial class Cartera : IEnumerable<Cliente>
	{
		public static Cartera Central = new Cartera();

		List<Cliente> clientes;
		private Cartera()
		{
			clientes = new List<Cliente>();
			Generar();
		}
		public void Agregar( Cliente cliente )
		{
			if( clientes.Contains( cliente ) ) return;
			clientes.Add( cliente );
		}
		public Cliente Buscar( string nombre )
		{
			return this.DefaultIfEmpty( Cliente.ConsumidorFinal ).FirstOrDefault( c => c.Contiene( nombre ) );
		}
		public IEnumerator<Cliente> GetEnumerator() => clientes.GetEnumerator();
	}
	partial class Cartera
	{
		private void Generar()
		{
			Agregar( new Cliente( "Alejandro", "Av Central" ) );
			Agregar( new Cliente( "Elidio", "Av Central" ) );
			Agregar( new Cliente( "Mirta", "Av Central" ) );
			Agregar( new Cliente( "Franco", "Solano Vera" ) );
			Agregar( new Cliente( "Maira", "Solano Vera" ) );
		}
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}

	public class Cliente
	{
		class Movimiento
		{
			public string Descripcion { get; private set; }
			public double Monto { get; private set; }
			public Movimiento( string descripcion, double monto )
			{
				Descripcion = descripcion;
				Monto = monto;
			}
			public override string ToString() => $"{Descripcion,-30} {Monto,8:C2}";
		}

		public string Nombre { get; private set; }
		public string Domicilio { get; private set; }

		List<Movimiento> movimientos;
		public Cliente( string nombre, string domicilio )
		{
			Nombre = nombre;
			Domicilio = domicilio;
			movimientos = new List<Movimiento>();
		}
		public static Cliente ConsumidorFinal = new Cliente( "CONSUMIDOR FINAL", "" );

		public void Comprar( string descripcion, double monto )
		{
			if( monto <= 0 ) return;

			movimientos.Add( new Movimiento( $"COMPRÓ: {descripcion}", -monto ) );
		}
		public void Pagar( string descripcion, double monto )
		{
			if( monto <= 0 ) return;
			movimientos.Add( new Movimiento( $"PAGÓ: {descripcion}", monto ) );
		}
		public double Saldo => movimientos.Sum( m => m.Monto );

		public bool Contiene( string texto ) => Nombre.Contains( texto ) || Domicilio.Contains( texto );

		public void Listar( Action<string> accion )
		{
			foreach( var movimiento in movimientos )
				accion( movimiento.ToString() );
		}
	}

	class Producto
	{
		public string Codigo { get; set; }
		public string Descripcion { get; set; }
		public double Precio { get; set; }

		public Producto( string codigo, string descripcion, double precio )
		{
			Codigo = codigo;
			Descripcion = descripcion;
			Precio = precio;
			movimientos = new List<int>();
		}
		List<int> movimientos;
		public void Agregar( int cantidad )
		{
			if( cantidad <= 0 ) return;
			movimientos.Add( cantidad );
		}

		public void Quitar( int cantidad )
		{
			if( cantidad <= 0 ) return;
			movimientos.Add( -cantidad );
		}

		public bool Contiene( string texto ) => Codigo == texto || Descripcion.Contains( texto );

		public int Existencia => movimientos.Sum();
		public override int GetHashCode() => Codigo.GetHashCode();
	}

	partial class Catalogo : IEnumerable<Producto>
	{
		public static Catalogo Central = new Catalogo();

		List<Producto> productos;
		private Catalogo()
		{
			productos = new List<Producto>();
			Generar();
		}
		public void Agregar( Producto producto )
		{
			if( productos.Contains( producto ) ) return;
			productos.Add( producto );
		}
		public Producto Buscar( string texto ) => productos.DefaultIfEmpty( null ).First( p => p.Contiene( texto ) );
		public IEnumerator<Producto> GetEnumerator() => productos.GetEnumerator();
	}

	partial class Catalogo
	{
		private void Generar()
		{
			Agregar( new Producto( "001", "Coca Cola", 50 ) );
			Agregar( new Producto( "002", "Fanta", 30 ) );
			Agregar( new Producto( "011", "Pepsi", 40 ) );
			Agregar( new Producto( "012", "Sprite", 45 ) );
			Agregar( new Producto( "021", "Sonrisas", 10 ) );
			Agregar( new Producto( "022", "Merengada", 12 ) );

			foreach( var producto in productos ) {
				producto.Agregar( 100 );
			}
		}
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
	partial class Factura
	{
		static int PróximaFactura = 1;
		int numero;
		class Detalle
		{
			public Producto Producto { get; private set; }
			public int Cantidad { get; set; }
			public double Importe => Producto.Precio * Cantidad;
			public Detalle(Producto producto, int cantidad )
			{
				Producto = producto;
				Cantidad = cantidad;
			}
			public override string ToString() => $"{( Cantidad < 0 ? "ANULAR: " : "" ) + Producto.Descripcion,-20} x {Cantidad,2} = {Importe,7:C2}";
		}

		List<Detalle> detalles;
		public string Numero => $"#A{numero,4:0000}";
		public DateTime Fecha { get; private set; }
		public Cliente Cliente { get; set; }

		public Factura()
		{
			Fecha = DateTime.Now;
			numero = PróximaFactura++;
			detalles = new List<Detalle>();
		}
		public void Agregar( Producto producto, int cantidad )
		{
			if( cantidad <= 0 || producto == null ) return;
			Registrar( producto, cantidad );
		}

		public void Quitar( Producto producto, int cantidad )
		{
			if( cantidad <= 0 || producto == null ) return;
			Registrar( producto, -Min( cantidad, Cantidad( producto ) ) );
		}

		public int Unidades => detalles.Sum( d => d.Cantidad );
		public double Total => detalles.Sum( d => d.Importe );

		public void Cerrar()
		{
			foreach( var detalle in detalles )
				detalle.Producto.Quitar( Cantidad( detalle.Producto ) );

			Cliente?.Comprar( $"Factura {Numero}", Total );
		}

		public void Listar( Action<string> accion )
		{
			foreach( var detalle in detalles )
				accion( detalle.ToString() );
		}
		public void Impactar( Action<Producto, int> accion )
		{
			foreach( var detalle in detalles )
				accion( detalle.Producto, Cantidad( detalle.Producto ) );
		}
	}
	partial class Factura
	{
		private Detalle Ultimo => detalles.Count == 0 ? null : detalles[ detalles.Count - 1 ];
		private int Cantidad( Producto producto ) => detalles.Where( d => d.Producto == producto ).Sum( d => d.Cantidad );
		private void Registrar( Producto producto, int cantidad )
		{
			if( Ultimo?.Producto == producto ) {
				Ultimo.Cantidad += cantidad;
				return;
			}
			if( Ultimo?.Cantidad == 0 ) detalles.Remove( Ultimo );
			detalles.Add( new Detalle( producto, cantidad ) );
		}
	}

	class Imprimir
	{
		public static void Mostrar( Factura factura )
		{
			var cliente = factura.Cliente ?? Cliente.ConsumidorFinal;
			Consola.Titulo( "Factura" );
			Consola.Mostrar( $"  Número    : {factura.Numero}" );
			Consola.Mostrar( $"  Cliente   : {cliente.Nombre}" );
			Consola.Mostrar( $"  Domicilio : {cliente.Domicilio}" );
			factura.Listar( linea => Consola.Mostrar( $"  · {linea}" ) );
			Consola.Mostrar( $"                        TOTAL : {factura.Total, 7:C2}" );
		}
		public static void Mostrar( Catalogo catalogo )
		{
			Consola.Titulo( "CATÁLOGO" );
			foreach( var producto in catalogo ) {
				Consola.Mostrar( $"  Código     : {producto.Codigo}" );
				Consola.Mostrar( $"  Descripción: {producto.Descripcion}" );
				Consola.Mostrar( $"  Precio     : {producto.Precio, 7:C2}" );
				Consola.Mostrar( $"  Existencia : {producto.Existencia,3}u" );
				Consola.Separar();
			}
		}
		public static void Mostrar( Cartera cartera )
		{
			Consola.Titulo( "CARTERA" );
			foreach( var cliente in cartera ) {
				Consola.Mostrar( $"  Nombre     : {cliente.Nombre}" );
				Consola.Mostrar( $"  Domicilio  : {cliente.Domicilio}" );
				Consola.Mostrar( $"  Saldo      : {cliente.Saldo, 9:C2}" );
				cliente.Listar( linea => Consola.Mostrar( $"  · {linea}" ) );
				Consola.Separar();
			}
		}
	}
	class DemoFactura
	{
		static void Main( string[] args )
		{
			var f1 = new Factura();
			var catalogo = Catalogo.Central;
			var cartera = Cartera.Central;

			f1.Cliente = cartera.Buscar( "Ale" );

			f1.Agregar( catalogo.Buscar( "Coca Cola" ), 2 );
			f1.Agregar( catalogo.Buscar( "Sonrisa" ), 2 );
			f1.Agregar( catalogo.Buscar( "Sonrisa" ), 2 );
			f1.Quitar( catalogo.Buscar( "Sonrisa" ), 3 );

			f1.Agregar( catalogo.Buscar( "002" ), 2 );
			f1.Quitar( catalogo.Buscar( "Coca Cola" ), 1 );

			f1.Cerrar();
			f1.Cliente.Pagar( "Pago a cuenta", 100 );
			Imprimir.Mostrar( f1 );
			Imprimir.Mostrar( catalogo );
			Imprimir.Mostrar( cartera );

			Consola.Pausa();
		}
	}
}
