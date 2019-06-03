using System;
using System.Collections.Generic;
using System.Text;

namespace Productos.Models
{
    public class Cuit
    {
        long valor;
        public Cuit(long cuit)
        {
            if (cuit < 20_00000000_0 || cuit > 34_99999999_9) throw new ArgumentOutOfRangeException();
            if (!EsValido(cuit))                              throw new ArgumentException();

            valor = cuit;
        }

        public override string ToString() => $"{valor:N13}";
        public override int GetHashCode() => valor.GetHashCode();

        public bool Equals(Cuit otro) => otro?.valor.Equals(valor) ?? false;
        public override bool Equals(object obj) => obj is Cuit otro ? Equals(otro) : false;
        public static bool operator ==(Cuit a, Cuit b) =>  a.Equals(b);
        public static bool operator !=(Cuit a, Cuit b) => !a.Equals(b);

        static bool EsValido(long numero) {
            int[] factores = { 2, 3, 4, 5, 6, 7, 2, 3, 4, 5 };

            var verificador = numero % 10;
            var suma = 0L;
            foreach (var factor in factores) {
                numero /= 10;
                suma += (numero % 10) * factor;
            }
            suma %= 11;

            var digito = suma == 1 ? 9 : (suma == 0 ? 0 : 11 - suma);
            return digito == verificador;
        }
    }
}
