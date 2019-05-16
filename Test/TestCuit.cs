using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Modelo.ValueObject;

namespace Test
{
    [TestFixture]
    public class TestCuit
    {
        [Test]
        public void CrearValido()
        {
            Assert.DoesNotThrow( ()=> new Cuit(20, 18627585, 4) );
        }

        [Test]
        public void TipoInvalida()
        {
            Assert.Throws<Exception>( ()=> new Cuit(33, 18627585, 4) );
        }
    }
}
