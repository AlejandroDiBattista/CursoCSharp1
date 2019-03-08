using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace DemoAgenda
{
    class Almacen
    {
        private TextReader Lector   { get; set; }
        private TextWriter Escritor { get; set; }
        public Almacen(TextReader lector, TextWriter escritor)
        {
            Lector   = lector;
            Escritor = escritor;
        }
        private FileStream file;
        public Almacen(string camino)
        {
            file = new FileStream(camino, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            Lector   = new StreamReader(file);
            Escritor = new StreamWriter(file);
        }
        public Agenda Leer()
        {
            file?.Seek(0, SeekOrigin.Begin);
            
            var json = Lector.ReadToEnd();
            //Console.WriteLine("Almacen.Leer");
            //Console.WriteLine(json);
            return JsonConvert.DeserializeObject<Agenda>(json);
        }

        public void Escribir(Agenda agenda)
        {
            var json = JsonConvert.SerializeObject(agenda, Formatting.Indented);
            //Console.WriteLine("Almacen.Escribir");
            //Console.WriteLine(json);
            file?.Seek(0, SeekOrigin.Begin);

            Escritor.Write(json);
            Escritor.Flush();
        }
    }
}
