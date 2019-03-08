using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DemoAgenda
{
    public static class Extensiones
    {
        public static bool Contiene(this string origen, string palabra) => origen.Palabras().Any(p => p.StartsWith(palabra));
        public static IEnumerable<String> Palabras(this string origen) => origen.Split(' ');
        public static bool Contiene(this IEnumerable<String> palabras, string origen) => palabras.All(p => origen.Contiene(p));
    }
}
