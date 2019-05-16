using System;
using Xunit;
using Modelo.ValueObject;

namespace DemoCuit.test
{
    public class CuitTest
    {
        [Fact]
        public void New_DeberiaCrearValido()
        {
            var esperado = "20-18627585-4";
            var real = new Cuit(20, 18627585, 4).ToString();
            Assert.Equal(esperado, real);
        }

        [Fact]
        public void New_DeberiaCrearValidoAunLosCortos()
        {
            var esperado = "27-05001941-7";
            var real = new Cuit(27, 5001941, 7).ToString();
            Assert.Equal(esperado, real);
        }

        [Fact]
        public void New_VerificiarTipos()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Cuit(19, 18627585, 4));
        }

        [Fact]
        public void New_VerificiarNumeroValido()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Cuit(20, -18627585, 4));
        }

        [Fact]
        public void New_VerificiarControlSeaDigito()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Cuit(20, 18627585, 10));
        }


        [Fact]
        public void New_VerificiarDigitoVerificador()
        {
            Assert.Throws<ArgumentException>(() => new Cuit(20, 18627585, 0));
        }


        [Fact]
        public void Parse_DeberiaAceptarDigitosPuros()
        {
            var esperado = "20-18627585-4";
            var real = Cuit.Parse("20186275854").ToString();
            Assert.Equal(esperado, real);
        }

        [Fact]
        public void Parse_DeberiaRechazarCaracteresExtraños()
        {
            Assert.Throws<ArgumentException>(() => Cuit.Parse("a018627585"));
        }

        [Fact]
        public void Parse_DeberiaIgnorarGuiones()
        {
            var esperado = "20-18627585-4";
            var real = Cuit.Parse("20-18627585-4").ToString();
            Assert.Equal(esperado, real);
        }

        [Fact]
        public void Parse_DeberiaAceptarCuitCortosSiUsaGuiones()
        {
            var esperado = "27-05001941-7";
            var real = Cuit.Parse("27-5001941-7").ToString();
            Assert.Equal(esperado, real);
        }

        [Fact]
        public void Parse_DeberiaRechazarCuitCortosSinGuiones()
        {
            Assert.Throws<ArgumentException>(() => Cuit.Parse("205000008"));
        }

        [Fact]
        public void Parse_DeberiaValidarParse()
        {
            Assert.True(Cuit.TryParse("27-5001941-7", out var cuit));
            Assert.Equal("27-05001941-7", cuit.ToString());
        }

        [Fact]
        public void Parse_DeberiaRechazarParse()
        {
            Assert.False(Cuit.TryParse("27-5001941-9", out var cuit));
            Assert.Null(cuit);
        }

        [Fact]
        public void Equal()
        {
            var a = Cuit.Parse("27-05001941-7");
            var b = new Cuit(27, 5001941, 7);
            Assert.Equal(a, b);
        }

        public void Equal_RechazarNulo()
        {
            var a = new Cuit(27, 5001941, 7);
            Assert.False(a.Equals(null));
        }

    }
}
