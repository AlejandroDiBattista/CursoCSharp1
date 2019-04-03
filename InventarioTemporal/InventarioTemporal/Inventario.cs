using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace InventarioTemporal
{
    [DataContract]
    public class Inventario
    {
        [DataMember]
        List<Producto> productos;

        public Inventario() => productos = new List<Producto>();
        public void Registrar(Producto producto) => productos.Add(producto);
        public Producto Buscar(string texto) => productos.FirstOrDefault(p => p.Descripcion.ToUpperInvariant().Contains(texto.ToUpperInvariant()));
        public IEnumerable<Producto> Productos() => productos;

        private const string Origen = @"..\..\..\inventario.json";
        public void Guardar()
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(Origen, json);
        }

        public static Inventario Cargar()
        {
            var texto = File.ReadAllText(Origen);
            var tmp = JsonConvert.DeserializeObject<Inventario>(texto);
            var ultimaHora = tmp.productos.Max(p => p.UltimoCambio);
            Reloj.Avanzar(ultimaHora);

            return tmp;
        }

        public static Inventario Generar()
        {
            var i = new Inventario();

            Reloj.Avanzar(0);
            i.Registrar(new Producto("Coca",  10, 50));
            i.Registrar(new Producto("Pepsi", 20, 40));
            i.Registrar(new Producto("Fanta", 30, 30));
            i.Registrar(new Producto("Crush", 40, 20));
            return i;
        }

    }
}
