using System;

namespace ArbolBinarioSimple
{
    public class Arbol
    {
        private int? dato;
        private Arbol izquierda, derecha;

        public void Agregar(int valor)
        {
            if(dato == null) {
                dato = valor;
            } else if(valor < dato) {
                if (izquierda == null) izquierda = new Arbol();
                izquierda.Agregar(valor);
            } else {
                ( derecha ?? (derecha = new Arbol()) ).Agregar(valor);
            }
        }
        public void Recorrer() {
            izquierda?.Recorrer();
            if (dato != null) Console.WriteLine($" - {dato}");
            derecha?.Recorrer();
        }
        public int Contar => (dato == null) ? 0 : (1 + (izquierda?.Contar ?? 0) + (derecha?.Contar ?? 0)) ;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" > DEMO Arbol Binario");
            var a = new Arbol();
            var r = new Random(1);
            for(var i = 0; i <= 20; i++) {
                a.Agregar( r.Next(10, 99) );
            }
            a.Recorrer();
            Console.WriteLine($" · Hay {a.Contar} valores");
            Console.ReadLine();
        }
    }
}
