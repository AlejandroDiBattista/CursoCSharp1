using System;
using static System.Console;
using System.Collections.Generic;

namespace DemoVarianza {
    class Program {

        class Padre {
        }

        class Hijo : Padre {
            public void Cantar() => WriteLine("ta ta tataann");
        }

        interface IComando<in T> {
            void Ejecutar(T t);
        }

        interface IConsulta<out T> {
            T Traer();
        }

        delegate void Mostrador(Hijo p);

        static void Mostrar(Padre x) => WriteLine(x);

        class ValueObject<T> where T : ValueObject<T> {
            public virtual IEnumerable<object> Campos() => null;
        }

        class Dinero: ValueObject<Dinero> {
            public string Moneda;
            public double Valor;
            public override IEnumerable<object> Campos() {
                return new [] { Moneda, Valor };
            }
        }
        static void Main() {
            WriteLine("Demo Varianza");

            var h = new Hijo();
            var p = new Padre();

            Mostrador m = Mostrar;

            m(new Hijo());

            WriteLine("Pulsar <Enter> para terminar"); ReadLine();
        }
    }
}
