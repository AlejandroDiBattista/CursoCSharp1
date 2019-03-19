using System;
using System.Text.RegularExpressions;

namespace ExpresionesRegulares
{
    class Program
    {
        const string Entrada = @"
        Gato: 
            1. Mamífero felino de tamaño generalmente pequeño, cuerpo flexible
            2. Instrumento compuesto por un engranaje de piñón y cremallera que sirve para levantar a poca altura grandes pesos
        
        Perro: 
            1. Mamifero Carnivoro domestico de la familia de lo cánidos
            2. Que es muy peresozo
            3. Que es muy desdichado
        ";
        const string HTML = @"antes cualquier cosa <div>Hola Mundo!</div> despues cualquier cosa";
        const string Tag  = @"(?<x><(?<a>\w+)>(?<body>.*?)(!?)</\k<a>>)";

        static void Main(string[] args)
        {
            Console.WriteLine("DEMO Expresiones Regulares");
            Regex r = new Regex(Tag, RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
            foreach(Match match in r.Matches(HTML))
            {
                var i = 0;
                Console.WriteLine(match.Groups["body"]);
                foreach (Group g in match.Groups)
                {
                    Console.WriteLine($"{i++}) Valor: [{g.Value}] en {g.Index}, Name: [{g.Name}]");
                };
            }
            
            Console.ReadLine();
        }
    }
}
