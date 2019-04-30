using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MiniEF
{

    class Producto
    {
        public Guid Id;
        public string Descripcion { get; set; }
        public double Precio { get; set; }

        public Producto(string descripcion, int precio)
        {
            Descripcion = descripcion;
            Precio = precio;
        }
    }

    interface IConjunto
    {
        List<string> Comandos { get; }
    }

    class Conjunto<T>: IConjunto
    {
        public List<string> Comandos { get; private set; }

        public Conjunto()
        {
            Comandos = new List<string>();
        }

        public void Agregar(T dato) {
            var pares = Valores(dato);
            pares["Id"] = $"'{Guid.NewGuid()}'";
            var campos  = String.Join(", ", pares.Keys);
            var valores = String.Join(", ", pares.Values);
            Comandos.Add($"INSERT INTO {Tabla} ({campos}) VALUES ({valores})");
        }

        public void Borrar(T dato) {  
          var pares = Valores(dato);
            Comandos.Add($"DELETE {Tabla} WHERE Id = {pares["Id"]}");
        }

        string Tabla => typeof(T).Name;

        Dictionary<string, string> Valores(T dato)
        {
            var salida = new Dictionary<string, string>();
            foreach(var propiedad in typeof(T).GetProperties())
            {
                var valor = propiedad.GetValue(dato);

                if(valor is Guid || valor is string) valor = $"'{valor}'";
                if (valor is Boolean) valor = ((bool)valor) ? "1" : "0";

                var name = propiedad.Name;
                salida[propiedad.Name] = valor.ToString();
            }
            return salida;
        }
    }

    class Contexto
    {
        public void Guardar() {
            foreach (var conjunto in Conjuntos)
            {
                var aux = conjunto.GetValue(this) as IConjunto;
                Console.WriteLine(String.Join("\n",aux.Comandos));
            }
        }

        protected IEnumerable<PropertyInfo> Conjuntos => GetType().GetProperties().Where(p => p.PropertyType.GetGenericTypeDefinition() == typeof(Conjunto<>));
    }

    class Mini : Contexto
    {
        public Conjunto<Producto> Productos { get; set; }
        public Conjunto<Producto> Productos2 { get; set; }

        public Mini()
        {
            foreach(var conjunto in Conjuntos)
            {
                var tmp = Activator.CreateInstance(conjunto.PropertyType);
                conjunto.SetValue(this, tmp);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DEMO");

            var m = new Mini();

            m.Productos.Agregar(new Producto("CocaZ", 80));
            m.Productos.Agregar(new Producto("Pepsi", 60));

            m.Guardar();

            Console.ReadLine();
        }
    }
}