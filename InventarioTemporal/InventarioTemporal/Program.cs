using System;
using static System.Console;

namespace InventarioTemporal
{
    
    class Program
    {
        static void Mostrar(string texto, ConsoleColor color = ConsoleColor.DarkGray )
        {
            var tmp = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine($"     {texto}");
            Console.ForegroundColor = tmp;
        }

        static void Main(string[] args)
        {
            var i = Inventario.Cargar();

            Producto producto = null;
            int      unidades = 0;
            double   precio = 0;
            int      hora = 0;

            var m = new Menu();
            m.Registrar("L", "Lista todos los productos.",       "LISTAR");
            m.Registrar("B", "Busca un producto.",               "BUSCAR");
            m.Registrar("C", "Compra unidades de un producto.",  "COMPRAR");
            m.Registrar("V", "Vende unidades de un producto.",   "VENDER");
            m.Registrar("P", "Fija precio de un producto.\n",    "PRECIO");
            m.Registrar("R", "", "RELOJ");
            m.Registrar("G", "", "GENERAR");
            m.Registrar("F", "Finaliza el programa.",            "FINALIZAR");

            while(true)
            {
                switch(m.Elegir())
                {
                    case "L":
                        foreach(var p in i.Productos())
                            WriteLine(p);
                        break;
                    case "R":
                        hora = m.Hora();
                        foreach (var p in i.Productos()) {
                            WriteLine(p.ToString(hora));
                            Mostrar(p.Historia());
                        }
                    break;

                    case "B":
                        producto = m.Producto(i);
                        WriteLine(producto);
                        break;

                    case "C":
                        producto = m.Producto(i);
                        unidades = m.Unidades();
                        producto.Comprar(unidades); ;
                        WriteLine(producto);
                        break;

                    case "V":
                        producto = m.Producto(i);
                        unidades = m.Unidades();

                        producto.Vender(unidades); ;
                        WriteLine(producto);
                        break;

                    case "P":
                        producto = m.Producto(i);
                        precio   = m.Precio();

                        producto.FijarPrecio(precio); ;
                        WriteLine(producto);
                        break;

                    case "G":
                        i = Inventario.Generar();
                        break;

                    case "F":
                        return;
                }
                m.Pausa();
                i.Guardar();
            }
        }
    }
}