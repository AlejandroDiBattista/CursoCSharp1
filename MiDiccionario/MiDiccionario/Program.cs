using System;
using System.Collections.Generic;
using System.IO;

namespace MiDiccionario
{

    class Diccionario {
        private class DicOrdenado : SortedList<string, List<string>> {
            public DicOrdenado() : base(StringComparer.CurrentCultureIgnoreCase) { }
        };
        DicOrdenado lista;

        public Diccionario()
        {
            this.lista = new DicOrdenado();
        }
        public void Definir(string palabra, string definicion)
        {
            if (!lista.ContainsKey(palabra)) lista[palabra] = new List<string>();
            lista[palabra].Add(definicion);
        }
        public IEnumerable<String>Buscar(string palabra)
        {
            return lista.ContainsKey(palabra) ? lista[palabra] : new List<string>();
        }
        public void Mostrar()
        {
            Console.WriteLine("DICCIONARIO");
            foreach (var (palabra, definiciones) in lista) {
                Console.WriteLine($" {palabra}:");
                var i = 1;
                foreach (var definicion in definiciones)
                    Console.WriteLine($"  {i++}. {definicion}");
                Console.WriteLine();
            }
        }
        public void Guardar(string camino) {
            var lineas = new List<string>();
            foreach(var (palabra, definiciones) in lista) {
                lineas.Add($"{palabra}:");
                var i = 1;
                foreach(var definicion in definiciones)
                {
                    var primero = true;
                    foreach(var linea in Diccionario.Acomodar(definicion, 30))
                    {
                        if (primero)
                            lineas.Add($"  {i++,2}. {linea}");
                        else
                            lineas.Add($"      {linea}");
                        primero = false;
                    }
                }    
                lineas.Add("");
            }
            File.WriteAllLines(camino, lineas);
        }

        public void Cargar(string camino) {
            var lineas = File.ReadAllLines(camino);
            lista.Clear();

            var palabra = "";
            var definicion = "";
            foreach(var l in lineas) {
                var aux = l.Trim();
                aux = aux.Replace("  ", " ");
                if (aux.Length > 0) {
                    if (aux.EndsWith(":")) {
                        palabra = aux.Substring(0, aux.Length - 1);
                        definicion = "";
                    } else {
                        var i = aux.IndexOf(".");
                        if (i > 0)
                            definicion = aux.Substring(i + 1).Trim();
                        else
                            definicion += " " + aux.Trim();
                    }
                } else {
                    Definir(palabra, definicion);
                }
            }   
        }
        public static IEnumerable<string> Acomodar(string parrafo, int maxLinea) {
            var lineas = new List<string>();
            var linea  = "";
            foreach (var palabra in parrafo.Split(" ")) {
                if (linea.Length + palabra.Length >= maxLinea) {
                    lineas.Add(linea.Trim());
                    linea = "";
                }
                linea += " " + palabra;
            }
            if (linea.Length > 0) lineas.Add(linea.Trim());
            return lineas;
        }
    }
    class Program
    {
    
        static void Main(string[] args)
        {
            var d = new Diccionario();
            d.Definir("Uno", "Primer numero");
            d.Definir("Uno", "El sucesor de cero");

            d.Definir("Dos", "Segundo numero");
            d.Definir("DOS", "El sucesor del sucesor de cero");

            d.Definir("Tres", "Tercer numero");
            d.Definir("Tres", "El sucesor del sucesor del sucesor de cero");

            d.Definir("Quince", "El quinceavo numero natural");
            d.Definir("Quince", "El sucesor del sucesor del sucesor del sucesor del sucesor del sucesor del sucesor del sucesor del sucesor del sucesor del sucesor del sucesor del sucesor del sucesor del sucesor de cero");

            //Console.WriteLine($"Definicion de {"DOS"} =");
            //Console.WriteLine(String.Join("\n", d.Buscar("Dos")));
            //Console.WriteLine("---");
            d.Mostrar();

            d.Guardar(@"..\..\..\dic.txt");
            d.Definir("NUEVO", "ESTE NO DEBE APARECER");
            d.Cargar(@"..\..\..\dic.txt");
            Console.WriteLine("CARGADO DE DISCO");
            d.Mostrar();
            Console.ReadLine();
        }
    }
}
