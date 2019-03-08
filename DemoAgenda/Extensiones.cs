using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DemoAgenda
{
    public static class Extensiones
    {
        public static IEnumerable<String> Palabras(this string origen) => origen.Split(' ');
        public static bool Contiene(this string origen, string palabra) => origen.Palabras().Any(p => p.StartsWith(palabra));
        public static bool Contiene(this string origen, IEnumerable<String> palabras) => palabras.All(p => origen.Contiene(p));
    }
}
