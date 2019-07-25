using System;

namespace PruebaEnum {

    public enum Verdad {
        Si,
        No,
    }

    //class Estado {
    //    object Antes;
    //    object Despues;
    //    public Estado(Enum valor) {
    //        Antes = valor;
    //    }
    //    public override string ToString() => Enum.GetName(Antes.GetType(), Antes);
    //}
    class Program {
        static void Main(string[] args) {

            Verdad v = Verdad.Si;
            var a = Enum.GetName(typeof(Verdad), v);
            Console.WriteLine($"Verdad: {a} => {v}");
            Console.ReadLine();
        }
    }
}