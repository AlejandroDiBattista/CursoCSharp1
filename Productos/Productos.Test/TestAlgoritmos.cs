using System;
using Xunit;
using Productos.Models;
using System.Linq;

namespace Productos.Test
{
    public class TestAlgoritmos
    {
        [Fact]
        public void Ean_Validos()
        {
            var a = new Ean(7790520017352);
            var b = new Ean(7792799000011);
        }

        [Fact]
        public void Ean_Invalidos()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Ean(7800520017350));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Ean(7700520017350));

            Assert.Throws<ArgumentException>(() => new Ean(7792799000010));
        }

        [Fact]
        public void Cuit_Validos()
        {
            var a = new Cuit(20186275854);
            var b = new Cuit(27050019417);
        }
        [Fact]
        public void Cuit_Invalidos()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Cuit(10186265850));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Cuit(40186265850));

            Assert.Throws<ArgumentException>(() => new Cuit(20186275850));
        }

        [Fact]
        public void Interes_Calcular33() {
            var importe = new Importe(100);
            var impInteres = new Importe(133.33);
            Assert.True(importe.AplicarInteres(1.0/3.0).Equals(impInteres));
        }
        [Fact]
        public void Interes_Calcular67() {
            var importe = new Importe(100);
            var impInteres = new Importe(166.67);
            Assert.True(importe.AplicarInteres(2.0 / 3.0).Equals(impInteres));
        }

        [Fact]
        public void Cuotas_Calcular() {
            var importe = new Importe(100);
            var aux = new Importe(0);
            foreach(var item in importe.Cuotas(3)) {
                aux += item;
            }
            Assert.True(importe.Equals(aux));
        }

        [Fact]
        public void Descuento_Calcular() {
            var importe = new Importe(100);
            Assert.Equal(importe,importe.AplicarInteres(0.1).AplicarDescuento(0.1));
        }
    }

}
