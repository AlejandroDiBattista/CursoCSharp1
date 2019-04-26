using System;
using SimpleInjector;


namespace DemoDI
{
    public interface IUno { void Mostrar(); }
    public interface IDos : IUno{ }

    public class Uno : IUno{
        public void Mostrar() => Console.WriteLine(" ·· UNO");
    }

    public class Dos : IDos {
        public Dos(IUno uno) { }
        public void Mostrar() => Console.WriteLine(" ·· DOS");
    }

    public class Doble {
        private IUno Uno;
        private IDos Dos;

        public Doble(IUno uno, IDos dos) {
            Uno = uno;
            Dos = dos;
        }

        public void Mostrar() {
            Console.WriteLine(" INICIO");
            Uno.Mostrar();
            Dos.Mostrar();
            Console.WriteLine(" FIN");
        }
    }

    public class Antes: IUno {
        public IUno Original;
        public Antes(IUno original) => Original = original;

        public void Mostrar() {
            Console.WriteLine(" · ANTES");
            Original.Mostrar();
        }
    }

    public class Despues : IUno {
        public IUno Original;
        public Despues(IUno original) => Original = original;

        public void Mostrar() {
            Original.Mostrar();
            Console.WriteLine(" · DESPUES");
        }
    }

    class Program {
        static void Main(string[] args) {
            Console.WriteLine("DEMO DI » Composicion");

            var c = new Container();
            c.Register<Doble>();

            c.Register<IUno, Uno>();
            c.Register<IDos, Dos>();

            c.RegisterDecorator<IUno, Despues>();
            c.RegisterDecorator<IUno, Antes>();

            var d = c.GetInstance<Doble>();
            d.Mostrar();

            Console.Write("Pulsa ENTER para terminar..."); Console.ReadLine();
        }
    }
}
