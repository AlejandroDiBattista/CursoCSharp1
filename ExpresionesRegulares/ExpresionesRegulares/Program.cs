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
        const string HTML = @"antes cualquier cosa <h2>Hola Mundo Cruel</h2> despues cualquier cosa <div>y esto</div> como se come";
        const string Tag  = @"<(?<etiqueta>\w+)>  
                                (?<body> (\w+(\s*))+) 
                              </\k<etiqueta>>";

        static void Main(string[] args)
        {
            Console.WriteLine("DEMO Expresiones Regulares");
            Console.WriteLine($" HTML: {HTML}");
            Console.WriteLine($" TAG : {Tag}");
            Regex r = new Regex(Tag, RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            foreach(Match match in r.Matches(HTML))
            {
                Console.WriteLine(match.Groups["body"]);
                foreach (Group g in match.Groups)
                {
                    Console.WriteLine($"Grupo: [{g.Value}] en {g.Index}, Name: [{g.Name}]");
                    foreach (Capture a in g.Captures)
                    {
                        Console.WriteLine($"   Captura: [{a.Value}] en {a.Index}");
                    }
                };
            }
            
            Console.ReadLine();
        }
    }
}
