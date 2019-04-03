using System;
using System.Collections.Generic;
using System.Text;
using  static System.Console;
using System.Linq;

namespace InventarioTemporal
{
    public class Menu
    {
        private class Comando
        {
            public string Letra;
            public string Descripcion;
            public string Accion;

            public Comando(string letra, string descripcion, string accion)
            {
                Descripcion = descripcion;
                Letra       = letra.ToUpper();
                Accion      = accion.ToUpper();
            }
        }

        List<Comando> Comandos = new List<Comando>();
        private string opcion;

        public void Registrar(string letra, string descripcion, string accion) => Comandos.Add(new Comando(letra, descripcion, accion));

        public string Elegir()
        {
            Clear();
            ForegroundColor = ConsoleColor.White;
            WriteLine();
            WriteLine($" MENU Inventario Temporal [{Reloj.Hora}]");
            foreach(var comando in Comandos)
                if(comando.Descripcion != "")
                    WriteLine($"  {comando.Letra} » {comando.Descripcion}");
            WriteLine();

            Comando eleccion = null;
            ForegroundColor = ConsoleColor.Green;
            Write("  Ingrese opción :> ");
            while((eleccion = Comandos.FirstOrDefault(c => c.Letra == opcion)) == null) {
                opcion = LeerLetra();
            };
            WriteLine();
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine($"\n > {eleccion.Accion}");

            return eleccion.Letra;
        }

        private string LeerLetra() => ReadKey().KeyChar.ToString().ToUpper();
        private string Leer(string mensaje)
        {
            ForegroundColor = ConsoleColor.Green;
            Write($"  Ingresar {mensaje,-12} :> ");
            var texto = ReadLine()?.Trim() ?? "";
            ForegroundColor = ConsoleColor.Yellow;
            return texto == "" ? Leer(mensaje) : texto;
        }

        public void Pausa()
        {
            ForegroundColor = ConsoleColor.Red;
            Write("\n  Pulsar una tecla para continuar...");
            opcion = LeerLetra();
            ForegroundColor = ConsoleColor.Yellow;
        }

        public Producto Producto(Inventario i)
        {
            return i.Buscar(Leer("Descripcion")) ?? Producto(i);
        }

        public int Unidades()
        {
            return int.TryParse(Leer("Unidades"), out int unidades) ? unidades : Unidades();
        }

        public int Hora()
        {
            return int.TryParse(Leer("Hora"), out int hora) ? hora : Hora();
        }

        public double Precio()
        {
            return double.TryParse(Leer("Precio"), out double precio) ? precio : Precio();
        }

    }

}
