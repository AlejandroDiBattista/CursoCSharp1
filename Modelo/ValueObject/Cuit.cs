using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace Modelo.ValueObject
{
    public class Cuit
    {
        private int Tipo;
        private int Numero;
        private int Control;

        public Cuit(int tipo, int numero, int control)
        {
            if (tipo != 20 && tipo != 23 && 
                tipo != 24 && tipo != 27 && 
                tipo != 30 && tipo != 33 && tipo != 34)
                throw new ArgumentOutOfRangeException("El tipo es incorrecto");
            if (numero < 1 || numero > 99_000_000)
                throw new ArgumentOutOfRangeException("El Numero es incorrecto");
            if (control < 0 || control > 9)
                throw new ArgumentOutOfRangeException("El control debe ser un digito");

            if (CalcularDigito(tipo * 100_000_000L + numero) != control)
                throw new ArgumentException("El digito verificador es invalido");

            Tipo    = tipo;
            Numero  = numero;
            Control = control;
        }

        public override string ToString() => $"{Tipo}-{Numero:D8}-{Control}";

        private static bool CuitValido(string cuit)
        {
            if (cuit.Length != 11) return false;

            var factores = new[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            var digitos  = cuit.Select(d => int.Parse(d.ToString()));

            int suma   = factores.Zip(digitos, (f, d) => f * d).Sum();
            int resto  = suma % 11;
            int digito = resto == 0 ? 0 : resto == 1 ? 9 : 11 - resto;

            return digitos.Last() == digito;
        }

        private static int CalcularDigito(long cuit)
        {
            var factores = new[] { 2, 3, 4, 5, 6, 7, 2, 3, 4, 5 };
            int suma = factores.Sum((factor) =>
            {
                var digito = (int)(cuit % 10);
                cuit /= 10;
                return digito * factor;
            });
            var resto = suma % 11;
            return resto == 0 ? 0 : resto == 1 ? 9 : 11 - resto;
        }

        public static Cuit Parse(string cuit)
        {
            if (Regex.IsMatch(cuit, @"\d{2}-\d{1,8}-\d"))
            {
                cuit = cuit.Replace("-", "");
            }
            else
            {
                if (!Regex.IsMatch(cuit, @"\d{11}"))
                    throw new ArgumentException("Debe ser 11 digitos");
            }
            int tipo    = int.Parse(cuit.Substring(0, 2));
            int numero  = int.Parse(cuit.Substring(2, cuit.Length - 3));
            int control = int.Parse(cuit.Substring(cuit.Length - 1, 1));

            return new Cuit(tipo, numero, control);
        }

        public static bool TryParse(string cuit, out Cuit salida)
        {
            try
            {
                salida = Cuit.Parse(cuit);
            }
            catch
            {
                salida = null;
                return false;
            }
            return true;
        }

        public bool Equals(Cuit otro)
        {
            if (otro == null)
                throw new NullReferenceException();
            return this.Tipo    == otro.Tipo   &&
                   this.Numero  == otro.Numero && 
                   this.Control == otro.Control;
        }

        public override bool Equals(object obj) => obj is Cuit otro ? Equals(otro) : false;
    }
}
