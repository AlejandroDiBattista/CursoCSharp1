using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace Entropia
{
    class Program
    {
        class Contador
        {
            private IDictionary<char, double> contar = new Dictionary<char, double>();
            public void Contar(string letras)
            {
                foreach (var letra in letras.ToCharArray())
                {
                    if (!contar.ContainsKey(letra)) contar[letra] = 0.0;
                    contar[letra] += 1.0;
                }
            }
            public double Total => contar.Values.Sum();
            public int Cantidad => contar.Keys.Count;
            public double Entropia => contar.Values.Select(f => f / Total).Sum(p => -p * Math.Log(p, 2));
        }

        static void Main()
        {
            Console.WriteLine("DEMO Entropía");

            var texto = File.ReadAllText(@"..\..\..\..\el_quijote.txt");
            texto = Regex.Replace(texto.ToLower(), @"[^a-z]", "");

            var c = new Contador();
            c.Contar(texto);

            Console.WriteLine($" · Hay {c.Cantidad} simbolos con una entropía de {c.Entropia, 0:N1} bits");
            Console.ReadLine();
        }
    }
}