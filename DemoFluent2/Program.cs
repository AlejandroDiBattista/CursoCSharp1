using System;
using System.Globalization;

namespace DemoFluent2
{
    public static class Program
    {
        static double Pesos(this double valor) => valor;
        static double Pesos(this int valor) => Pesos((double)valor);

        static double Centavos(this double valor) => valor/100;
        static double Centavos(this int valor) => Centavos((double)valor);

        static double Dolares(this double valor) => valor * 50;
        static double Dolares(this int valor) => Dolares((double)valor);

        static void Main(string[] args)
        {
            Console.WriteLine("Demo Fluent");
            var a = 10.Pesos() + 12.Centavos() + 300.Dolares();

            a = 1234.567;
            Mostrar(a);

            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("es-ES");
            Mostrar(a);

            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("sv-SE");
            Mostrar(a);

            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("fr-FR");
            Mostrar(a);

            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            Mostrar(a);

            Console.ReadLine();
        }

        private static void Mostrar(double a)
        {
            Console.WriteLine();
            Console.WriteLine($"{CultureInfo.CurrentCulture.Name} > {CultureInfo.CurrentCulture.NativeName}");
            Console.WriteLine($"· $10 + $0,12 + u$s300 >>");
            Console.WriteLine($" ¥ > {a}");
            Console.WriteLine($"  > {a:C2}");
            Console.WriteLine($"  > {String.Format(CultureInfo.GetCultureInfo("sv-SE"),"{0}",a)}");
            Console.WriteLine($"  > {String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:C2}", a)}");

            Console.WriteLine($"  > {a.ToString(CultureInfo.GetCultureInfo("sv-SE"))}");
            Console.WriteLine($"  > {a.ToString("C2", CultureInfo.GetCultureInfo("sv-SE"))}");
            Console.WriteLine();
            return;
            Console.WriteLine($"· Hoy >> ");
            Console.WriteLine($"  > {DateTime.Now}");
            Console.WriteLine($"  > d       {DateTime.Now:d}");
            Console.WriteLine($"  > f       {DateTime.Now:f}");
            Console.WriteLine($"  > g       {DateTime.Now:g}");
            Console.WriteLine($"  > G       {DateTime.Now:G}");
            Console.WriteLine($"  > d|M        {DateTime.Now:d|M}");
            Console.WriteLine($"  > dd|MM      {DateTime.Now:dd|MM}");
            Console.WriteLine($"  > ddd|MMM    {DateTime.Now:ddd|MMM}");
            Console.WriteLine($"  > dddd|MMMM  {DateTime.Now:dddd|MMMM}");
            Console.WriteLine($"Saludo :{"Hola Ñato"}" );
        }
    }
}
