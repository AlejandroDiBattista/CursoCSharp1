using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace DemoAgenda
{
    enum TipoEntrada { Telefono, Email, Domicilio}
    [DataContract]
    class Entrada
    {
        [DataMember]
        [JsonConverter(typeof(StringEnumConverter))]
        public TipoEntrada Tipo { get; set; }
        [DataMember]
        public string Valor { get; set; }
        public Entrada(TipoEntrada tipo, string valor)
        {
            Tipo = tipo;
            Valor = valor;
        }
        public bool Contiene(string texto) => Valor.Contains(texto);
        public override string ToString() => $"{Tipo}: {Valor}";
    }
}
