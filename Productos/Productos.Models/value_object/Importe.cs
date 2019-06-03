using System;
using System.Collections.Generic;
using System.Text;

namespace Productos.Models
{
    public class Importe
    {
        double valor;

        public Importe(double precio) {
            if (precio < 0)        throw new Exception();
            if (precio % 0.01 > 0) throw new Exception();

            valor = precio;
        }

        public Importe Sumar(Importe importe) => new Importe(valor + importe.valor);
        public static Importe operator +(Importe a, Importe b) => a.Sumar(b);

        public override string ToString() => $"{valor:C2}";
        public override int GetHashCode() => valor.GetHashCode();

        public bool Equals(Importe otro) => otro?.valor.Equals(valor) ?? false;
        public override bool Equals(object obj) => obj is Importe otro ? Equals(otro) : false;
        public static bool operator ==(Importe a, Importe b) =>  a.Equals(b);
        public static bool operator !=(Importe a, Importe b) => !a.Equals(b);
    }

}
