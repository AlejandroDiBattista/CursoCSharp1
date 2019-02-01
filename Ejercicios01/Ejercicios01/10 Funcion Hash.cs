using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ejercicios10
{
	static class DemoHash
	{

		class Punto
		{
			public int X { get; set; }
			public int Y { get; set; }

			//public override int GetHashCode() => X.GetHashCode() + 37 * Y.GetHashCode();

			//public override int GetHashCode()
			//{
			//	var h = new HashCode();
			//	h.Add( X );
			//	h.Add( Y );
			//	return h.ToHashCode();
			//}
		}

		static void Main( string[] args )
		{

			Console.WriteLine("* DEMO GetHashCode *");

			var a = new Punto { X = 0, Y = 0 };
			Console.WriteLine($"a(0,0){a.GetHashCode()}");

			var b = new Punto { X = 0, Y = 0 };
			Console.WriteLine( $"b(0,0){b.GetHashCode()}" );

			var c = new Punto { X = 1, Y = 0 };
			Console.WriteLine( $"c(1,0){c.GetHashCode()}" );

			var d = new Punto { X = 0, Y = 1 };
			Console.WriteLine( $"d(0,1){d.GetHashCode()}" );
			var e = d;
			Console.WriteLine( $"e(d){d.GetHashCode()}" );

			Console.ReadLine();
		}
	}
}
