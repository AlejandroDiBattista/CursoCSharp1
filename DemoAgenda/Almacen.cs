using System;
using System.IO;
using Newtonsoft.Json;

namespace DemoAgenda
{
    class Almacen
    {
        private Stream file;
        public TextReader Lector { get; set; }
        private TextWriter Escritor { get; set; }

        private Almacen(Stream file, TextReader lector, TextWriter escritor)
        {
            this.file = file;
            Lector = lector;
            Escritor = escritor;
        }

        public Almacen(TextReader lector, TextWriter escritor)
            : this(null, lector, escritor)
        {
        }

        public Almacen(string camino)
            : this(CreateFileStream(camino), null, null)
        {
            Lector = new StreamReader(file);
            Escritor = new StreamWriter(file);
        }

        public Almacen(Stream origen)
            : this(origen, new StreamReader(origen), new StreamWriter(origen))
        {
        }

        public Agenda Leer()
        {
            file?.Seek(0, SeekOrigin.Begin);
            var json = Lector.ReadToEnd();
            return JsonConvert.DeserializeObject<Agenda>(json);
        }

        public void Escribir(Agenda agenda)
        {
            Escribir(agenda, Escritor);
        }

        public void Escribir(Agenda agenda, TextWriter escritor)
        {
            var json = JsonConvert.SerializeObject(agenda, Formatting.Indented);
            file?.Seek(0, SeekOrigin.Begin);
            escritor.Write(json);
            escritor.Flush();
        }

        private static FileStream CreateFileStream(string camino)
        {
            return new FileStream(camino, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }
    }
}
