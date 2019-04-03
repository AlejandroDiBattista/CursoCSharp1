using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Linq;

namespace InventarioTemporal
{
    [DataContract]
    public class Producto
    {
        class Cambios<T>
        {
            public T Valor;
            public int Hora;

            public Cambios(T valor)
            {
                Valor = valor;
                Hora = Reloj.Hora;
            }
        }

        [DataMember]
        public string Descripcion { get; private set; }

        public int Existencia() => existencias.Sum(e => e.Valor);
        public int Existencia(int hora) => existencias.Where(e => e.Hora <= hora).Sum(e => e.Valor);

        public double Precio() => precios.Select(m => m.Valor).LastOrDefault();
        public double Precio(int hora) => precios.Where(e => e.Hora <= hora).Select(m => m.Valor).LastOrDefault();

        [DataMember]
        private List<Cambios<int>> existencias = new List<Cambios<int>>();
        [DataMember]
        private List<Cambios<double>> precios = new List<Cambios<double>>();

        public Producto(string descripcion, int unidades, double precio)
        {
            Descripcion = descripcion;
            Comprar(unidades);
            FijarPrecio(precio);
        }

        public void Comprar(int unidades) => existencias.Add(new Cambios<int>(unidades));
        public void Vender(int unidades) => existencias.Add(new Cambios<int>(-unidades));
        public void FijarPrecio(double precio) => precios.Add(new Cambios<double>(precio));

        public int UltimoCambio => Math.Max(existencias.Max(m => m.Hora), precios.Max(m => m.Hora));
        public override string ToString() => $" · {Descripcion,-20}  {Existencia(),4}  ${Precio(),5:N2}  @{UltimoCambio}";
        public string ToString(int hora)  => $" · {Descripcion,-20}  {Existencia(hora),4}  ${Precio(hora),5:N2} @{hora}";
        public string Historia() => String.Join(" » ", existencias.Select(m => $"{m.Valor}@{m.Hora}")) + " | " + 
                                    String.Join(" » ", precios.Select(m => $"${m.Valor:N2}@{m.Hora}"));
    }
}
