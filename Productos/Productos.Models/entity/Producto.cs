using System;
using System.Collections.Generic;
using System.Text;

namespace Productos.Models
{
    public class Producto
    {
        public Ean Codigo { get; private set; }
        public string Descripcion { get; private set; }
        public Importe Precio { get; set; }

        public Producto(Ean codigo, string descripcion, Importe precio) {
            if (codigo == null)         throw new ArgumentNullException();
            if (descripcion == null)    throw new ArgumentNullException();
            if (descripcion.Length < 1) throw new ArgumentException();
            if (precio == null)         throw new ArgumentNullException();

            Codigo = codigo;
            Descripcion = descripcion;
            Precio = precio;
        }

        public override string ToString() => $"{Codigo} {Descripcion, -30} {Precio} ";
        public override int GetHashCode() => Codigo.GetHashCode();

        public bool Equals(Producto otro) => otro?.Codigo.Equals(Codigo) ?? false;
        public override bool Equals(object obj) => obj is Producto otro ? Equals(otro) : false;
        public static bool operator ==(Producto a, Producto b) =>  a.Equals(b);
        public static bool operator !=(Producto a, Producto b) => !a.Equals(b);

        public static Producto Nulo() => new Producto(new Ean(0), "", 0);
    }
}
