using System;
using System.Collections.Generic;
using System.Linq;

namespace Fluente
{
    namespace Modelo
    {
        public class Cliente
        {
            public string Nombre { get; set; }
            public string Direccion { get; set; }
        }

        public class Producto
        {
            public string Descripcion { get; set; }
            public double Precio { get; set; }
        }

        public class Compra
        {
            public double Precio { get; set; }
            public int Cantidad { get; set; }
            public Producto Producto { get; set; }
            public double Importe => Cantidad * Precio;
        }

        public class Factura
        {
            public Cliente Cliente { get; set; }
            private List<Compra> compras = new List<Compra>();
            public double Total => compras.Sum(c => c.Importe);

            public Factura() { }
            public void Agregar(Compra compra) => compras.Add(compra);

        }
    }
}
