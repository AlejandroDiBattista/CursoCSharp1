using System;
using SimpleInjector;

namespace DemoDI
{
    public interface IUno { void Mostrar(); }
    public interface IDos : IUno{ }

    public class Uno : IUno {
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

    public class Duplicar : IDos
    {
        public IDos Original;
        public Duplicar(IDos original) => Original = original;

        public void Mostrar()
        {
            Console.Write(" 1."); Original.Mostrar();
            Console.Write(" 2."); Original.Mostrar();
        }
    }

    class Program {
        
        static void Main(string[] args) {
            Console.Clear();
            Console.WriteLine("DEMO DI\n\n");

            Console.WriteLine(" » Configurar \n");
            var c = new Container();
            c.Register<Doble>();

            c.RegisterDecorator<IUno, Despues>();
            c.RegisterDecorator<IUno, Antes>();

            c.RegisterDecorator<IDos, Duplicar>();

            c.Register<IUno, Uno>(Lifestyle.Singleton);
            c.Register<IDos, Dos>();
            c.Verify();

            Console.WriteLine(">>> Con ID");
            var d = c.GetInstance<Doble>();
            d.Mostrar();


            Console.WriteLine("\n\n>>> A mano");

            var n1 = new Uno();
            var ds = new Despues(n1);
            var an = new Antes(ds);
            var n2 = new Dos(an);
            var e = new Doble(an, n2);

            e.Mostrar();
            Console.Write("\nPulsa ENTER para terminar..."); Console.ReadLine();
        }
    }
}
