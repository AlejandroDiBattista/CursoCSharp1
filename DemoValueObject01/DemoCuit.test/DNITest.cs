using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Modelo.ValueObject;

namespace DemoValueObject01.DemoCuit.Test
{
    public class TestDNI
    {
        [Fact]
        public void New_Crear()
        {
            var d = new DNI(18627585);
        }

        [Fact]
        public void New_RechazarInvalidos()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DNI(-10));
            Assert.Throws<ArgumentOutOfRangeException>(() => new DNI(99_000_000));
        }

        [Fact]
        public void DebeMostrarSalidaFormateada()
        {
            var a = new DNI(18627585);
            Assert.Equal("18.627.585", a.ToString());
        }

        [Fact]
        public void ParseValido()
        {
            var a = DNI.Parse("18.627.585");
            Assert.Equal("18.627.585", a.ToString());

            var b = DNI.Parse("18627585");
            Assert.Equal("18.627.585", a.ToString());

            var c = DNI.Parse("0018627585");
            Assert.Equal("18.627.585", a.ToString());
        }

        [Fact]
        public void ParseRechazarCaracteresRaros()
        {
            Assert.Throws<ArgumentException>(() => DNI.Parse("18-627-585"));
            Assert.Throws<ArgumentException>(() => DNI.Parse("dni 18627585"));
            Assert.Throws<ArgumentException>(() => DNI.Parse("018627595"));
        }

    }
}
