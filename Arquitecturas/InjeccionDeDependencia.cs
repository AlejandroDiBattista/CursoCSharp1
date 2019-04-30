using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SimpleInjector;

namespace Arquitecturas
{ 
    interface IConeccion{
        void Ejecutar(string sql);
    }

    interface IComando {
        string Tabla { get; set; }
        void Agregar(string campo, object valor);
        void Ejecutar();
    }

    interface IAgregarComando    : IComando {}
    interface IActualizarComando : IComando {}
    interface IBorrarComando     : IComando {}

    class Coneccion : IConeccion
    {
        public void Ejecutar(string sql) => Console.WriteLine($"   » EJECUTANDO » {sql}");
    }

    class MostrarComando : IComando
    {
        private IComando Comando;
        public MostrarComando(Comando comando) => Comando = comando;

        public string Tabla {
            get => Comando.Tabla;
            set => Comando.Tabla = value;
        }

        public void Agregar(string campo, object valor) => Comando.Agregar(campo, valor);

        public void Ejecutar()
        {
            Console.WriteLine("ANTES");
            Comando.Ejecutar();
            Console.WriteLine("DESPUES");
        }
    }
    abstract class Comando : IComando {
        public IConeccion Coneccion { get; set; }

        public string Tabla { get ; set ; }
        protected Dictionary<string, object> valores = new Dictionary<string, object>();

        public Comando(IConeccion coneccion) => Coneccion = coneccion;

        public void Agregar(string campo, object valor)
        {
            if (valor is String || valor is Guid) valor = $"'{valor}'";
            valores[campo] = valor;
        }

        protected abstract string GenerarSQL { get; }
        public void Ejecutar() => Coneccion.Ejecutar(GenerarSQL);

        protected string Campos  => String.Join(", ", valores.Keys);
        protected string Valores => String.Join(", ", valores.Values);
        protected string Pares   => String.Join(", ", valores.Select((item) => $"{item.Key} = {item.Value}"));
    }

    class AgregarComando: Comando, IAgregarComando {
        public AgregarComando(IConeccion coneccion) : base(coneccion){}
        protected override string GenerarSQL => $"INSERT INTO {Tabla} ({Campos}) VALUES ({Valores})";
    }

    class ActualizarComando : Comando, IActualizarComando{
        public ActualizarComando(IConeccion coneccion) : base(coneccion) { }
        protected override string GenerarSQL => $"UPDATE {Tabla} SET {Pares} WHERE id = {valores["id"]}";
    }

    class BorrarComando : Comando, IBorrarComando {
        public BorrarComando(IConeccion coneccion) : base(coneccion) { }
        protected override string GenerarSQL => $"DELETE {Tabla} WHERE id = {valores["id"]}";
    }

    static class Storage
    {
        static void Guardar(object entidad)
        {
            IComando cmd;
           

 
            cmd.Ejecutar();

        }
    }

    class Producto
    {
        public Guid? ID { get; private set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }

        public Producto(Guid? id, string descripcion, double precio)
        {
            ID = id;
            Descripcion = descripcion;
            Precio = precio;
        }

        void PonerOferta() => Precio *= 0.9;

        public void Guardar()
        {
            IComando cmd;
            if(ID == null)
                cmd = Demo.container.GetInstance<IAgregarComando>();
            else
                cmd = Demo.container.GetInstance<IActualizarComando>();

            ID = ID ?? Guid.NewGuid();

            cmd.Tabla = nameof(Producto);

            cmd.Agregar("id", ID);
            cmd.Agregar("descripcion", Descripcion);
            cmd.Agregar("precio", Precio);

            cmd.Ejecutar();
        }

        public void Borrar()
        {
            IComando cmd = Demo.container.GetInstance<IBorrarComando>();

            cmd.Tabla = nameof(Producto);
            cmd.Agregar("id", ID);
            cmd.Ejecutar();
        }
    }

    static class Demo
    {
        public static readonly Container container;
        static Demo()
        {
            //Console.WriteLine("Constructor static Demo");
            container = new Container();
            container.RegisterSingleton<IConeccion, Coneccion>();
            container.Register<IAgregarComando,     AgregarComando>();
            container.Register<IActualizarComando,  ActualizarComando>();
            container.Register<IBorrarComando,      BorrarComando>();
            //container.RegisterDecorator<IComando,   MostrarComando>();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("DEMO Injeccion de dependencia");
            var p = new Producto(Guid.Empty, "Coca Cola", 100);

            Console.WriteLine("\n · Guardar (1º vez)..."); 
            Storage.Guardar(p);

            Console.ReadLine();

            Console.WriteLine("\n · Guardar (2º vez)...");
            p.Guardar();
            Console.ReadLine();

            Console.WriteLine("\n · Borrar...");
            p.Borrar();

            Console.Write("\nPulsar ENTER parar terminar...");Console.ReadLine();
        }

    }
}
