using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DemoAgenda
{
    class Contacto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Nacimiento { get; set; }

        IList<Entrada> entradas;
        public bool Contiene(string palabra) => Nombre.Contains(palabra) || Apellido.Contains(palabra) || entradas.Any( e => e.Contiene(palabra));
        public bool Contiene(string[] palabras) => palabras.All(palabra => Contiene(palabra));
    }
}
