using System;
using static System.Console;

namespace DemoValueObject01
{
    class Program
    {

        public static int CalcularDigitoCuit(string cuit)
        {
            int[] mult = new[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            char[] nums = cuit.ToCharArray();
            int total = 0;
            for (int i = 0; i < mult.Length; i++)
            {
                total += int.Parse(nums[i].ToString()) * mult[i];
            }
            var resto = total % 11;
            return resto == 0 ? 0 : resto == 1 ? 9 : 11 - resto;
        }

        static void Main(string[] args)
        {
            WriteLine("DEMO > VO > Cuit");
            Write("Ingrese CUIL (XX-XXXXXXXX-X) :");
            var cuit = ReadLine();
            cuit = cuit.Replace("-", "");
            WriteLine();
            var digito = Int32.Parse(cuit.Substring(10, 1));
            if (digito == CalcularDigitoCuit(cuit.Substring(0, 10)))
            {
                WriteLine(" El Cuit es valido :)");
            }
            else
            {
                WriteLine(" El Cuit NO es valido :(");
            }
            WriteLine();
            Write("Pulse INTRO para terminar"); ReadLine();
        }
    }
}
