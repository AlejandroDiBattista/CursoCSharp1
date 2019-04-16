using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace DemoHTTP
{

   class RangoAttribute : Attribute
   {
      public int Minimo { get; set; }
      public int Maximo { get; set; }

      //public RangoAttribute(int minimo, int maximo)
      //{
      //   Minimo = minimo;
      //   Maximo = maximo;
      //}

      public RangoAttribute(int minimo, int maximo)
      {
         Minimo = minimo;
         Maximo = maximo;
      }


      public int Variacion() => Maximo - Minimo;
   }

   class Persona
   {
      [Rango(10, 60)]
      public int Edad { get; set; }
      public bool Vivo { get; set; }

      [Rango(10000, 20000)]
      public int Sueldo { get; set; }
      public Persona(int Edad, bool Vivo)
      {
         this.Edad = Edad;
         this.Vivo = Vivo;
      }
      public Persona Matar()
      {
         Vivo = false;
         return this;
      }
      public Persona CumplirAño()
      {

         var r = typeof(Persona)
            .GetProperties()
            .Where(p => p.Name == nameof(Edad))
            .FirstOrDefault()
            .CustomAttributes.OfType<RangoAttribute>()
            .FirstOrDefault();

         if (Edad < r.Maximo)
         {
            Edad += 1;
         }

         return this;
      }
      public Persona Aumentar(int incremento)
      {
         var r = typeof(Persona)
            .GetProperties()
            .Where(p => p.Name == nameof(Sueldo))
            .FirstOrDefault()
            .CustomAttributes.OfType<RangoAttribute>()
            .FirstOrDefault();

         var nuevo = Sueldo + incremento;
         if(nuevo >= r.Minimo & nuevo <= r.Maximo)
            Sueldo = nuevo;
         return this;
      }

      static void Main(string[] args)
      {
         var a = new Persona(50, true);
         a.CumplirAño().Matar().CumplirAño().CumplirAño();

         a.CumplirAño();
         a.Matar();
         a.CumplirAño();
         a.CumplirAño();
      }
   }
}
