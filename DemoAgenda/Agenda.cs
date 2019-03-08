using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.Serialization;

namespace DemoAgenda
{
    [DataContract]
    class Agenda //: IEnumerable<Contacto>
    {
        [DataMember]
        IList<Contacto> contactos;

        public Agenda() => contactos = new List<Contacto>();

        public void Agregar(Contacto contacto) => contactos.Add(contacto);
        Contacto Buscar(string texto) => BuscarTodos(texto).FirstOrDefault();
        public IEnumerable<Contacto> BuscarTodos(string texto) => contactos.Where(c => texto.Palabras().All(palabra => c.Contiene(palabra)));
        //public IEnumerator<Contacto> GetEnumerator() => contactos.GetEnumerator();
        //IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        public int Cantidad => contactos.Count;
    }
}
