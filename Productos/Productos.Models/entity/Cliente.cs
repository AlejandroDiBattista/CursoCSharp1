using System;
using System.Collections.Generic;
using System.Text;
using Productos.Models;

namespace Productos.Models.entity
{
    public class Direccion
    {
        public string Calle;
        public int Altura;
        public string Localidad;
        public int CodigoPostal;
        public string Provincia;
    }

    public class Cliente
    {
        public Cuit Cuit { get; private set; }
        public string Nombre { get; set; }
        public Direccion Domicilio { get; set; }    
        public Cliente(Cuit cuit, string nombre) {
            if (cuit == null)      throw new ArgumentNullException();
            if (nombre == null)    throw new ArgumentNullException();
            if (nombre.Length < 1) throw new ArgumentException();

            Cuit   = cuit;
            Nombre = nombre;
        }

        public override string ToString() => $"{Cuit} {Nombre,-30}";
        public override int GetHashCode() => Cuit.GetHashCode();

        public bool Equals(Cliente otro) => otro?.Cuit.Equals(Cuit) ?? false;
        public override bool Equals(object obj) => obj is Cliente otro ? Equals(otro) : false;
        public static bool operator ==(Cliente a, Cliente b) => a.Equals(b);
        public static bool operator !=(Cliente a, Cliente b) => !a.Equals(b);
    }
}
