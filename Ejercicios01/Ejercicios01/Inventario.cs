using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;

namespace Ejercicios07
{
	class Producto
	{
		public string Codigo { get; }
		public string Nombre { get; set; }
		public double Precio { get; set; }

		public Producto(string codigo, string nombre, double precio )
		{
			Codigo = codigo;
			Nombre = nombre;
			Precio = precio;
		}

		public override int GetHashCode() => Codigo.GetHashCode();
		public override string ToString() => $"{Codigo,2} - {Nombre,-10} {Precio,5}";
		public override bool Equals( object obj ) => Codigo.Equals( ((Producto)obj).Codigo );
	}

	class Inventario : ICollection<Producto>
	{
		Dictionary<Producto, int> existencias = new Dictionary<Producto, int>();

		public int Count => existencias.Count;

		public bool IsReadOnly => false;

		public void Add( Producto item )
		{
			if( !existencias.ContainsKey( item ) ) 
				existencias.Add( item, 0 );

			existencias[ item ] += 1;
		}

		public void Clear() => existencias.Clear();

		public bool Contains( Producto item ) => existencias.ContainsKey( item );

		public void CopyTo( Producto[] array, int arrayIndex ) => throw new NotImplementedException();

		public IEnumerator<Producto> GetEnumerator() => existencias.Keys.OrderBy( i => i.Codigo ).GetEnumerator();

		public bool Remove( Producto item )
		{
			if( !existencias.ContainsKey( item ) || existencias[ item ] == 0 ) return false;

			existencias[ item ] -= 1;
			return true;
		}

		public int Cantidad( Producto item ) => existencias.ContainsKey( item ) ? existencias[ item ] : 0;
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		public double Valuacion() => this.Select( i => i.Precio * Cantidad( i ) ).Sum();
		public int Unidades() => this.Select( i => Cantidad( i ) ).Sum();
	}
	class Program
	{
		static void Main( string[] args )
		{
			var p1 = new Producto("1", "iPhone",   800.00);
			var p2 = new Producto("2", "iPad",    1000.00);
			var p3 = new Producto("3", "MacBook", 2000.00);

			Console.WriteLine("DEMOSTRACIÓN IMPLEMENTACIÓN ICollection<>");
			var inventario = new Inventario();
			inventario.Add( p1 );
			Debug.Assert( inventario.Cantidad( p1 ) == 1 );

			inventario.Add( p1 );
			p1.Precio = 700;  // Al cambiar el precio no debe afectar el Inventario
			Debug.Assert( inventario.Cantidad( p1 ) == 2 );

			inventario.Add( p2 );
			Debug.Assert( inventario.Cantidad( p2 ) == 1 );

			inventario.Remove( p1 );
			Debug.Assert( inventario.Cantidad( p1 ) == 1 );

			Console.WriteLine( "\n - LISTADO DE EXISTENCIAS - " );
			foreach( var producto in inventario ) 
				Console.WriteLine( $" · {producto} * { inventario.Cantidad( producto ),3}u " );

			Console.ReadLine();
		}
	}
}
