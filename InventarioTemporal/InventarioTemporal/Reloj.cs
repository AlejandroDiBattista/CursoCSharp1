using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioTemporal
{
    static class Reloj
    {
        private static int horaActual = 0;
        public static int Hora => horaActual++;
        public static int Actual => horaActual;
        public static void Avanzar(int hora) => horaActual = hora + 1;
    }
}
