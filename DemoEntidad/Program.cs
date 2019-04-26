using System;
using System.Linq;
using System.Collections.Generic;
using SimpleInjector;

namespace DemoEntidad
{
    class Producto {
        public Guid ID { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }

        public void PonerOferta()  => Precio /= 1.1;
        public void QuitarOferta() => Precio *= 1.1;
    }

    interface IConeccion {
        void Ejecutar(string sql);
        IEnumerable<object> Consultar(string sql);
    }

    class Coneccion : IConeccion {
        public void Ejecutar(string sql) => Console.WriteLine($"Ejecutando > {sql}");
        public IEnumerable<object> Consultar(string sql)
        {
            yield return new { Guid = "", Descripcion = "Coca Cola",  Precio = 80 };
            yield return new { Guid = "", Descripcion = "Pepsi Cola", Precio = 70 };
        }
    }

    class Productos {
        IConeccion Coneccion;

        public Productos(IConeccion coneccion) => Coneccion = coneccion;

        public Producto Crear() => new Producto();

        public void Agregar(Producto producto){
            producto.ID = Guid.NewGuid();
            var campos  = String.Join(",", producto.GetType().GetProperties().Select(p => p.Name));
            var valores = String.Join(",", producto.GetType().GetProperties().Select(p => p.ToString() ));

            var sql = $"INSERT INTO {nameof(Productos)} ({campos}) SET ({valores})";
            Coneccion.Ejecutar(sql);
        }

        public void Actualizar(Producto producto) {
            var campos  = producto.GetType().GetProperties().Where(p=> p.Name != "ID").Select(p => p.Name);
            var valores = producto.GetType().GetProperties().Where(p => p.Name != "ID").Select(p => p.ToString());
            var pares   =  String.Join(",", campos.Zip(valores, (c, v) => $"{c} = {v}"));

            var sql = $"UPDATE {nameof(Productos)} SET {pares} WHERE ID = '{producto.ID}";
            Coneccion.Ejecutar(sql);
        }

        public void Borrar(Producto producto) {
            var sql = $"DELETE {nameof(Productos)} WHERE ID = '{producto.ID}')";
            Coneccion.Ejecutar(sql);
        }

        public IEnumerable<Producto>Traer(string SQL) {
            var lista = Coneccion.Consultar($"SELECT * FROM {nameof(Productos)}");

            foreach(var item in lista) {
                var tmp = new Producto();
                foreach(var campo in tmp.GetType().GetProperties())
                    campo.SetValue(tmp, campo.GetValue(item) );
                yield return tmp;
            }
        }

    }
    class Program {
        static void Pausa() {
            Console.Write("Pulse ENTER para continuar..."); Console.ReadLine();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("DEMO >> DbSet");

            var contenedor = new SimpleInjector.Container();
            contenedor.Register<IConeccion, Coneccion>(Lifestyle.Singleton);
            contenedor.Register<Productos>(Lifestyle.Singleton);

            var productos = contenedor.GetInstance<Productos>();

            var a = productos.Crear();
            a.Descripcion = "Coca cola";
            a.Precio = 90;

            productos.Agregar(a);
            Pausa();

            a.PonerOferta();
            productos.Actualizar(a);
            Pausa();

            productos.Borrar(a);
            Pausa();

            var ps = productos.Traer("SELECT * FROM Productos");
        }
    }
}
