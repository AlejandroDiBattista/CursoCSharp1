using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DemoAgenda
{
    class Almacen
    {
        private TextReader Lector { get; set; }
        private TextWriter Escritor { get; set; }
        public Almacen(TextReader lector, TextWriter escritor)
        {
            Lector = lector;
            Escritor = escritor;
        }
        public Agenda Leer() => new Agenda(Lector.ReadToEnd());
        public void Escribir(Agenda agenda) {
            Escritor.Write(agenda.ToString());
        }
    }
}
