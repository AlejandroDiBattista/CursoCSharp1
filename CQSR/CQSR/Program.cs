using System;

namespace CQSR
{
    class Persona
    {
        public int Edad { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DEMO CQSR");

            var p = new Persona();
            p.Edad = 51;

            Console.WriteLine(p.Edad);
            Console.ReadLine();
        }
    }
}
