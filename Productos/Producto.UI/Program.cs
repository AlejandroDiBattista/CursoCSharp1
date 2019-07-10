using System;

namespace Producto.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            double a = 123.45000;
            bool b = ((a*100) - Math.Truncate(a * 100)) == 0;
            Console.WriteLine($"{a} > {a % 1.0} >> {a % 0.1} >>> {a % 0.01}" );
            Console.WriteLine($"{b}");
            Console.ReadLine();
        }
    }
}
