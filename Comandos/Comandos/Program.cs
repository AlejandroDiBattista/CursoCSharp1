using System;
using System.Collections.Generic;
using System.Linq;

namespace Comandos {
    class Cuenta {
        public string Descripcion { get; set; }
        public double Saldo { get; set; }

        public Cuenta(string descripcion, double saldo) {
            this.Descripcion = descripcion;
            this.Saldo = saldo;
        }

        public void Depositar(double monto) {
            if (monto < 0) return;
            Saldo += monto;
        }

        public void Extraer(double monto) {
            if (monto >= Saldo || monto < 0) return;
            Saldo -= monto;
        }
        
        public void Transferir(Cuenta destino, double monto) {
            this.Extraer(monto);
            destino.Depositar(monto);
        }
        public override string ToString() => $"{Descripcion} = ${Saldo, 3}";
     }

    abstract class Comando {
        public bool Completo { get; set; }
        public abstract void Ejecutar();
        public abstract void Deshacer();
    }

    class DepositarComando : Comando {
        private Cuenta Destino;
        private double Monto;
        public DepositarComando(Cuenta destino, double monto) {
            Destino = destino;
            Monto = monto;
        }

        public override void Ejecutar() {
            Destino.Saldo += Monto;
            Completo = true;
        }
        public override void Deshacer() {
            Destino.Saldo -= Monto;
            Completo = false;
        }

    }
    class ExtraerComando : Comando {
        private Cuenta Destino;
        private double Monto;
        public ExtraerComando(Cuenta destino, double monto) {
            Destino = destino;
            Monto = monto;
        }
        public override void Ejecutar() {
            Destino.Saldo -= Monto;
            Completo = true;
        }
        public override void Deshacer() {
            Destino.Saldo += Monto;
            Completo = false;
        }
    }

    class TransferirComando : Comando {

        private Cuenta Origen, Destino;
        private double Monto;
        public TransferirComando(Cuenta origen, Cuenta destino, double monto) {
            Origen  = origen;
            Destino = destino;
            Monto   = monto;
        }

        public override void Ejecutar() {
            Origen.Saldo -= Monto;
            Destino.Saldo += Monto;
            Completo = true;
        }

        public override void Deshacer() {
            Origen.Saldo += Monto;
            Destino.Saldo -= Monto;
            Completo = false;
        }
    }

    class Transacciones {
        IList<Comando> comandos = new List<Comando>();
        public void Agregar(Comando comando) {
            comandos.Add(comando);
        }
        public IEnumerable<Comando> Pendientes() => comandos.Where(c => !c.Completo);
        public void EjecutarTodo() {
            foreach (var cmd in Pendientes())
                cmd.Ejecutar();
        }
        public void Ejecutar() {
            comandos.Where(cmd => !cmd.Completo).FirstOrDefault()?.Ejecutar();
        }
        public void Deshacer() {
            comandos.Where(cmd => cmd.Completo).LastOrDefault()?.Deshacer();
        }
    }

    class Program {
        static void Main(string[] args) {
            Console.WriteLine("DEMO Cuenta");
            var c = new Cuenta("CC#1", 100);
            var o = new Cuenta("CA#2", 0);

            if (true) {
                var T = new Transacciones();
                Console.WriteLine($" » {c} | {o}");
                T.Agregar(new DepositarComando(c, 100));
                T.Agregar(new ExtraerComando(c, 50));
                T.Agregar(new TransferirComando(c, o, 30));
                Console.WriteLine($" » {c} | {o}");
                T.EjecutarTodo();
                Console.WriteLine($" » {c} | {o}");
                T.Deshacer();
                Console.WriteLine($" » {c} | {o}");
            } else {
                Console.WriteLine($" » {c} | {o}");
                c.Depositar(100);
                Console.WriteLine($" » {c} | {o}");
                c.Extraer(50);
                Console.WriteLine($" » {c} | {o}");
                c.Transferir(o, 30);
                Console.WriteLine($" » {c} | {o}");
            }
            Console.ReadLine();
        }
    }
}