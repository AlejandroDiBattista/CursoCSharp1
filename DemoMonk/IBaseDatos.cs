using System;
using System.Collections.Generic;

namespace DemoMonk
{
    public interface IBaseDatos
    {
        void Abrir();
        void Cerrar();
        IEnumerable<object> Consultar(string SQL);
        void Executar(string SQL);
    }
}

