using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Productos.Models
{
    public class Ean
    {
        long valor;

        public Ean(long ean) { 
            if (ean < 779_000000000_0 || ean > 779_999999999_9) throw new ArgumentOutOfRangeException();
            if (!Ean.EsValido(ean))                             throw new ArgumentException();

            valor = ean;
        }

        public override string ToString()       => $"{valor:N13}";
        public override int GetHashCode()       => valor.GetHashCode();

        public bool Equals(Ean otro) => otro?.valor.Equals(valor) ?? false;
        public override bool Equals(object obj) => obj is Ean otro ? Equals(otro) : false;
        public static bool operator ==(Ean a, Ean b) =>  a.Equals(b);
        public static bool operator !=(Ean a, Ean b) => !a.Equals(b);

        static bool EsValido(long numero) {
            int[] factores = { 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3 };

            var verificador = numero % 10;
            var suma = 0L;
            foreach (var factor in factores) {
                numero /= 10;
                suma += (numero % 10) * factor;
            }
            suma %= 10;

            var digito = suma == 0 ? 0 : 10 - suma;
            return digito == verificador;
        }
    }
}
