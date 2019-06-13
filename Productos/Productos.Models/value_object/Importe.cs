using System;
using System.Collections.Generic;
using System.Text;

namespace Productos.Models {
    public class Importe {
        double valor;

        public Importe(double precio) {
            if(precio < 0) throw new Exception();
            //if(precio % 0.01 > 0) throw new Exception();

            valor = precio;
        }

        public Importe Sumar(Importe importe) => new Importe(valor + importe.valor);

        public static Importe operator +(Importe a, Importe b) => a.Sumar(b);

        public override string ToString() => $"{valor:C2}";
        public override int GetHashCode() => valor.GetHashCode();

        public override bool Equals(object obj) => obj is Importe otro ? Equals(otro) : false;

        public bool Equals(Importe otro) => otro?.valor.Equals(valor) ?? false;
        public static bool operator ==(Importe a, Importe b) => a.Equals(b);
        public static bool operator !=(Importe a, Importe b) => !a.Equals(b);

        public static implicit operator Importe(double valor) => new Importe(valor);

        public Importe AplicarInteres(double interes) =>
            new Importe(Math.Round(valor * (1.0 + interes), 2));
        // new Importe(100).AplicarInteres(1.0/3.0) == new Importe(133.33)
        // new Importe(100).AplicarInteres(2.0/3.0) == new Importe(166.67)

        public IEnumerable<Importe> Cuotas(int cuotas) {
            var acumulado = 0.0;
            var cuota = Math.Round(valor / cuotas, 2);
            for(int i = 1; i < cuotas; i++) {
                acumulado += cuota;
                yield return new Importe(cuota);
            }
            yield return new Importe(valor - acumulado);
        }
        // new Importe(100).Cuotas(3) => [33.33, 33.33, 33.34]; 
        public Importe AplicarDescuento(double descuento) {
            return new Importe(Math.Round(valor / (1.0 + descuento), 2));
        }
    }

    class Descuento : Importe {
        public Descuento(double valor) : base(valor) {
        }
    }
}
