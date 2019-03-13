using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.Serialization;

namespace DemoAgenda
{
    [DataContract]
    class Contacto
    {
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Apellido { get; set; }
        [DataMember]
        public DateTime Nacimiento { get; set; }
        [DataMember]
        IList<Entrada> entradas;

        public Contacto() => entradas = new List<Entrada>();
        public Contacto( string nombre, string apellido, string telefono=null, string email=null, string domicilio = null) : this()
        {
            Apellido = apellido;
            Nombre   = nombre;
            if (telefono  != null) Agregar(new Entrada(TipoEntrada.Telefono,  telefono));
            if (email     != null) Agregar(new Entrada(TipoEntrada.Email,     email));
            if (domicilio != null) Agregar(new Entrada(TipoEntrada.Domicilio, domicilio));
        }
        public void Agregar(Entrada entrada) => entradas.Add(entrada);
        public bool Contiene(string palabra) => Nombre.Contiene(palabra) || Apellido.Contiene(palabra) || entradas.Any( e => e.Contiene(palabra));
        public override string ToString() => $"{Apellido}, {Nombre} [{String.Join(", ", entradas.Select(e => e.ToString()))}]";
    }
}
