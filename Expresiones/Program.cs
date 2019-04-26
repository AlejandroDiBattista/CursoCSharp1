using System;
using System.Collections.Generic;

namespace Expresiones
{
   public abstract class Expresion
   {
      public abstract int Evaluar();
      public abstract string NPI();
      public virtual void Mostrar(int nivel = 0) => Separar(nivel);
      public void Separar(int nivel = 0) => Console.Write(new String(' ', 1 + nivel * 2));

      static public implicit operator Expresion(int valor) => new Constante(valor);
      public static Expresion operator +(Expresion izquierda, Expresion derecha) => new Suma(izquierda, derecha);
      public static Expresion operator +(string nombre, Expresion derecha) => new Suma(new Variable(nombre), derecha);
      public static Expresion operator *(Expresion izquierda, Expresion derecha) => new Producto(izquierda, derecha);
      public static Expresion operator <(Expresion izquierda, Expresion derecha) => new Menor(izquierda, derecha);
      public static Expresion operator >(Expresion izquierda, Expresion derecha) => new Menor(derecha, izquierda);
   }

   public class Constante : Expresion
   {
      public virtual int Valor { get; set; }
      public Constante(int valor) => Valor = valor;

      public override int Evaluar() => Valor;
      public override string ToString() => $"{Valor}";
      public override string NPI() => $"{Valor}";
      public override void Mostrar(int nivel = 0) {
         base.Mostrar(nivel);
         Console.WriteLine(Valor);
      }

   }

   public abstract class Binaria : Expresion
   {
      public Expresion Izquierda { get; set; }
      public Expresion Derecha { get; set; }
      public String Operador { get; set; }

      public Binaria(string operador, Expresion izquierda, Expresion derecha)
      {
         Operador = operador;
         Izquierda = izquierda;
         Derecha = derecha;
      }
      public override string ToString() => $"{Izquierda} {Operador} {Derecha}";
      public override string NPI() => $"({Operador} {Izquierda.NPI()} {Derecha.NPI()})";
      public override void Mostrar(int nivel = 0)
      {
         Separar(nivel); Console.WriteLine(Operador);
         Izquierda.Mostrar(nivel + 1);
         Derecha.Mostrar(nivel + 1);
      }
   }

   public class Suma : Binaria
   {
      public Suma(Expresion izquierda, Expresion derecha) : base("+", izquierda, derecha) { }
      public override int Evaluar() => Izquierda.Evaluar() + Derecha.Evaluar();
   }

   public class Producto : Binaria
   {
      public Producto(Expresion izquierda, Expresion derecha) : base("*", izquierda, derecha) { }
      public override int Evaluar() => Izquierda.Evaluar() * Derecha.Evaluar();
   }

   public class Menor : Binaria
   {
      public Menor(Expresion izquierda, Expresion derecha) : base("<", izquierda, derecha) { }
      public override int Evaluar() => Izquierda.Evaluar() < Derecha.Evaluar() ? 1 : 0;
   }

   public class Variable : Expresion
   {
      static Dictionary<string, int> variables = new Dictionary<string, int>();
      public string Nombre { get; set; }
      public Variable(string nombre) => Nombre = nombre;
      public Variable(string nombre, int valor) : this(nombre) => variables[nombre] = valor;
      static public implicit operator Variable(string nombre) => new Variable(nombre);

      public int Valor {
         get => variables[Nombre];
         set => variables[Nombre] = value;
      }
      public override int Evaluar() => Valor;
      public override string ToString() => $"{Nombre}";
      public override string NPI() => $"{Nombre}";
      public override void Mostrar(int nivel = 0)
      {
         Separar(nivel); Console.WriteLine(Nombre);
      }
   }

   public class Asignar : Binaria
   {
      public Asignar(Variable variable, Expresion expresion = null) : base("=", variable, expresion) { }
      public override int Evaluar()
      {
         if (Izquierda is Variable v)
         {
            v.Valor = Derecha.Evaluar();
            return v.Valor;
         }
         return 0;
      }
   }

   public class Condicional : Binaria
   {
      public Expresion Condicion { get; set; }
      public Condicional(Expresion condicion, Expresion Correcto, Expresion Incorrecto = null) : base("IF", Correcto, Incorrecto)
      {
         Condicion = condicion;
      }
      public override int Evaluar() => Condicion.Evaluar() != 0 ? Izquierda.Evaluar() : Derecha?.Evaluar() ?? 0;
      public override string ToString()
      {
         if(Derecha is null)
            return $"IF({Condicion}, {Izquierda})";
         else
            return $"IF({Condicion}, {Izquierda}, {Derecha})";
      }

      public override void Mostrar(int nivel = 0)
      {
         Separar(nivel); Console.WriteLine("IF");
         Condicion.Mostrar(nivel + 1);
         Separar(nivel); Console.WriteLine("THEN");
         Izquierda.Mostrar(nivel + 1);
         if (Derecha != null)
         {
            Separar(nivel); Console.WriteLine("ELSE");
            Derecha.Mostrar(nivel + 1);
         }
         Separar(nivel); Console.WriteLine("END");
      }
   }

   public class Repeticion : Binaria
   {
      public Repeticion(Expresion condicion, Expresion Cuerpo) : base("WHILE", condicion, Cuerpo){}
      public override int Evaluar()
      {
         var resultado = 0;
         while (Izquierda.Evaluar() != 0)
            resultado = Derecha.Evaluar();
         return resultado;
      }
      public override string ToString() => $"WHILE({Izquierda}) {{ {Derecha} }}";
      public override void Mostrar(int nivel = 0)
      {
         Separar(nivel); Console.WriteLine("WHILE");
         Izquierda.Mostrar(nivel + 1);
         Separar(nivel); Console.WriteLine("DO");
         Derecha.Mostrar(nivel + 1);
         Separar(nivel); Console.WriteLine("END");
      }

   }

   static class Program
   {

      static void Main(string[] args)
      {
         Console.WriteLine("DEMO Expresion!");
         var x = new Variable("x", 100);
         Expresion e = x + 5 * 20;
         Console.WriteLine();
         Console.WriteLine($"Evaluar | {e} => {e.Evaluar()}");
         Console.WriteLine();
         Console.WriteLine(e.NPI());
         Console.WriteLine();
         e.Mostrar();
            Console.ReadLine();
            return;
         Console.WriteLine();
         Expresion contador = new Asignar(x, x + 1);
         Console.WriteLine("> CONTADOR");
         contador.Mostrar();
         Console.WriteLine();
         Console.WriteLine(contador.NPI());
         Console.WriteLine($"Contador | {contador} => {contador.Evaluar()}");
         Console.WriteLine($"Contador | {contador} => {contador.Evaluar()}");
         Console.WriteLine($"Contador | {contador} => {contador.Evaluar()}");

         Console.WriteLine();
         var repetir = new Repeticion(new Menor(x, 20), contador);
         Console.WriteLine("> REPETICION");
         Console.WriteLine(repetir.NPI());
         repetir.Mostrar();

         Console.WriteLine($"Repeticion | {repetir} => {repetir.Evaluar()}");
         Console.ReadLine();
      }
   }
}
