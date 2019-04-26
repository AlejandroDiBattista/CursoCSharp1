using System;
using SimpleInjector;


namespace DemoDI
{
    public interface IUno { void Mostrar(); }
    public interface IDos : IUno{ }

    public class Uno : IUno{
        public void Mostrar() => Console.WriteLine(" · UNO");
    }

    public class Dos : IDos {
        public Dos(IUno uno) { }
        public void Mostrar() => Console.WriteLine(" · DOS");
    }

    public class Doble {
        private IUno Uno;
        private IDos Dos;

        public Doble(IUno uno, IDos dos) {
            Uno = uno;
            Dos = dos;
        }

        public void Mostrar() {
            Console.WriteLine(" ► Inicio");
            Uno.Mostrar();
            Dos.Mostrar();
            Console.WriteLine(" ■ Fin");
        }
    }

    public class Antes: IUno {
        public IUno Original;
        public Antes(IUno original) => Original = original;

        public void Mostrar() {
            Console.WriteLine(" ↓ antes");
            Original.Mostrar();
        }
    }

    public class Despues : IUno {
        public IUno Original;
        public Despues(IUno original) => Original = original;

        public void Mostrar() {
            Original.Mostrar();
            Console.WriteLine(" ↑ despues ");
        }
    }

    class Program {
        static void Main(string[] args) {
            Console.WriteLine("DEMO DI » Composicion\n");

            var c = new Container();
            c.Register<Doble>();

            c.Register<IUno, Uno>(Lifestyle.Singleton);
            c.Register<IDos, Dos>();

            c.RegisterDecorator<IUno, Despues>();
            c.RegisterDecorator<IUno, Antes>();

            var d = c.GetInstance<Doble>();
            d.Mostrar();

            Console.Write("\nPulsa ENTER para terminar..."); Console.ReadLine();
        }
    }
}
