using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DemoAgenda
{
    class Agenda : IEnumerable<Contacto>
    {
        IList<Contacto> contactos;

        public Agenda() => contactos = new List<Contacto>();
        public Agenda(string json) : this() { }
        public void Agregar(Contacto contacto) => contactos.Add(contacto);
        Contacto Buscar(string texto) => BuscarTodos(texto).FirstOrDefault();
        IEnumerable<Contacto> BuscarTodos(string texto) => this.Where(c => c.Contiene(texto.Split(' ')));
        public IEnumerator<Contacto> GetEnumerator() => contactos.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
