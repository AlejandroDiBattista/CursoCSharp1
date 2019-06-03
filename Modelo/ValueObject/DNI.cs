using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Modelo.ValueObject
{
    public class DNI
    {
        private int _dni;
        public DNI(int dni) {
            if (dni < 1 || dni > 99_000_000)
                throw new ArgumentOutOfRangeException();
            _dni = dni;
        }

        public override string ToString() => $"{_dni:###,###,##0}";

        public bool Equals(DNI otro) => otro != null ? this._dni == otro._dni : false ;
        public override bool Equals(object obj) => obj is DNI dni ? Equals(dni) : false;

        public static DNI Parse(string dni)
        {
            if (!Regex.IsMatch(dni, @"[1-9]\d+") &&
                !Regex.IsMatch(dni, @"[1-9]\d{0,2}(\.\d{3}){0,2}"))
                throw new ArgumentException();
            dni = dni.Replace(".", "");
            return new DNI(int.Parse(dni));
        }

        public static bool TryParse(string dni, out DNI salida) {
            try
            {
                salida = DNI.Parse(dni);
            }
            catch
            {
                salida = null;
                return false;
            }
            return true;
        }
    }
}
