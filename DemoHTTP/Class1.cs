using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace DemoHTTP {
    class RangoValidoAttribute : Attribute{
        public int Minimo { get; set; }
        public int Maximo { get; set; }

        public RangoValidoAttribute(int min, int max){
            Minimo = min;
            Maximo = max;
        }

        public int Rango => Maximo - Minimo;
    }

    [DataContract]
    class Persona {
        private int _edad;

        [RangoValido(18,65)]
        [DataMember]
        public int Edad
        {
            get {
                return _edad;
            }
            set {
                var a = this.GetType().GetProperty(nameof(Edad)).CustomAttributes.OfType<RangoValidoAttribute>().FirstOrDefault();
                if(value >= a.Minimo && value <= a.Maximo)
                    _edad = value;
            }
        }
        [DataMember]
        public bool Vivo { get; set; }

        [DataMember]
        [RangoValido(20000,  80000)]
        public int Sueldo { get; set; }

        public Persona(int Edad, bool Vivo){
            this.Edad = Edad;
            this.Vivo = Vivo;
        }

        public Persona Matar(){
            Vivo = false;
            return this;
        }

        public Persona CumplirAño(){
            var r = this.GetType().GetProperty(nameof(Edad)).CustomAttributes.OfType<RangoValidoAttribute>().FirstOrDefault();

            if (Edad < r.Maximo)
                Edad += 1;

            return this;
        }
        public Persona Aumentar(int incremento){
            var r = this.GetType().GetProperty(nameof(Sueldo)).CustomAttributes.OfType<RangoValidoAttribute>().FirstOrDefault();

            var nuevo = Sueldo + incremento;
            if (nuevo >= r.Minimo & nuevo <= r.Maximo)
                Sueldo = nuevo;

            return this;
        }

        static void Main(string[] args){
            var a = new Persona(50, true);
            a.CumplirAño().Matar().CumplirAño().CumplirAño();

            var r = typeof(Persona).GetProperty(nameof(Edad)).CustomAttributes.OfType<RangoValidoAttribute>().FirstOrDefault();
            Console.WriteLine($"Edad: Min{r.Minimo}, Max: {r.Maximo}");
            a.CumplirAño();
            a.Matar();
            a.CumplirAño();
            a.CumplirAño();
        }
    }
}
