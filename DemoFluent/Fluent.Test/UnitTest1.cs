using System;
using Xunit;
using NFluent;
using Fluent.Test;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace Fluent.Test
{
    public class Cliente
    {
        public string Nombre    { get; private set; }
        public string Direccion { get; private set; }

        public Cliente(string nombre, string direccion)
        {
            if (nombre == null)    throw new Exception();
            if (direccion == null) throw new Exception();

            Nombre    = nombre;
            Direccion = direccion;
        }
    }

    public class Producto
    {
        public string Descripcion { get; private set; }
        public Producto(string descripcion)
        {
            if (descripcion == null) throw new Exception();
            Descripcion = descripcion;
        }
    }

    public class Compra
    {
        public double Precio { get; private set; }
        public int Cantidad { get; private set; }
        public Producto Producto { get; private set; }

        public double Importe => Cantidad * Precio;

        public Compra(int cantidad, Producto producto, double precio)
        {
            if (cantidad < 0)     throw new Exception();
            if (producto == null) throw new Exception();
            if (precio <= 0)      throw new Exception();

            Cantidad = cantidad;
            Producto = producto;
            Precio   = precio;
        }
    }

    public interface ICliente
    {
        ICliente Nombre(string nombre);
        Factura Direccion(string direccion);
    }

    public class AuxCliente : ICliente
    {
        private Factura factura;
        private string nombre = "";
        public AuxCliente(Factura factura) => this.factura = factura;

        public ICliente Nombre(string nombre)
        {
            this.nombre = nombre;
            return this;
        } 

        public Factura Direccion(string direccion)
        {
            factura.cliente = new Cliente(nombre, direccion);
            return factura;
        }
    }

    public class AuxCompra
    {
        private Factura factura;
        private int cantidad;
        private Producto producto;
        
        public AuxCompra(Factura factura) => this.factura = factura;

        public AuxCompra Cantidad(int cantidad)
        {
            this.cantidad = cantidad;
            return this;
        }

        public AuxCompra Producto(string descripcion)
        {
            this.producto = new Producto(descripcion);
            return this;
        }
        public Factura Precio(double precio)
        {
            factura.Agregar(new Compra(cantidad, producto, precio));
            return factura;
        }

    }
    public class Factura
    {
        public Cliente cliente { get; set; }
        private List<Compra> compras = new List<Compra>();
        public double Total => compras.Sum(c => c.Importe);
        public int Unidades => compras.Sum(c => c.Cantidad);

        public void Agregar(Compra compra) => compras.Add(compra);

        public Factura() { }
        public ICliente Cliente() => new AuxCliente(this);

        public AuxCompra Comprar => new AuxCompra(this);
    }

    public class FacturaTest
    {
        [Fact]
        public void Crear()
        {
            //CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("es-AR");

            Check.That(12345.678.ToString("C2")).IsEqualTo("12,32");
            var f = new Factura().
                Cliente().
                    Nombre("Alejandro").
                    Direccion("Avda Central").
                Comprar.
                    Cantidad(1).
                    Producto("Coca Cola").
                    Precio(10).
                Comprar.
                    Cantidad(2).
                    Producto("Pepsi Cola").
                    Precio(20).
                Comprar.
                    Cantidad(3).
                    Producto("Fanta").
                    Precio(30).
                Comprar.
                    Cantidad(4).
                    Producto("Sprite").
                    Precio(20);

            //var f = new Factura();
            //f.Cliente = new Cliente( "Alejandro", "Avda. Central 4124" );
            //f.Agregar(new Compra(1, new Producto("Coca Cola"), 10));
            //f.Agregar(new Compra(2, new Producto("Pepsi Cola"), 20));
            //f.Agregar(new Compra(3, new Producto("Fanta"), 30));


            Check.That(f.Total).IsNotZero();
            Check.That(f.Total).IsStrictlyGreaterThan(100);

            Check.That(f.Total).IsStrictlyPositive();
            Check.ThatCode( () => f.Total / 0 ).Throws<Exception>();

            Assert.True(f.Total > 0);


            Check.That(f.Total).IsEqualTo(140);
            Check.That(f.Unidades).IsStrictlyGreaterThan(1).And.IsStrictlyLessThan(10);
        }
    }
}
