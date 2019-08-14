using System;
using System.Collections.Generic;
using System.Text;

namespace Cola {
    public class Registro {

        public class Movimiento {
            public DateTime Hora;
            public int Esperando;
        }

        TimeSpan AvanceReloj = new TimeSpan(0);
        List<Movimiento> movimientos = new List<Movimiento>();

        public Registro() { }

        public void Configurar(int cantidad) { }
        public void Configurar(int actual, int turno) { }

        public void Avanzar() { }
    }
}
