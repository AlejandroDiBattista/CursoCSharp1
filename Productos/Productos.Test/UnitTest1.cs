using System;
using Xunit;
using Productos.Models;

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
    }

}
