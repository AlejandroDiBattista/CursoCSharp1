using System;
using System.Collections.Generic;
using System.Text;

namespace DemoAgenda
{
    enum TipoEntrada { Telefono, Email, Domicilio}
    class Entrada
    {
        public TipoEntrada Tipo { get; set; }
        public string Valor { get; set; }
        public bool Contiene(string texto) => Valor.Contains(texto);
    }
}
