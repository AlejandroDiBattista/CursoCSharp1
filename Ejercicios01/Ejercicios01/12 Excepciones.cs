using System;
using System.Collections.Generic;
using System.Text;

namespace Ejercicios12
{
	
	public class Persona
	{
		public string Nombre { get; set; }
		public int Edad { get; set; }
		public override string ToString() => $"Nombre: {Nombre} Edad: {Edad}";
	}

	public class Empresa
	{
		public int Catalogo { get; set; }
		public int CuentaCorriente { get; set; }

		public Persona Usuario { get; }
		public bool Activo { get; set; }
		private Empresa()
		{
			Console.WriteLine("Hola estoy en EMPRESA");
			Usuario = new Persona() { Nombre = "Alejandro", Edad = 51 };
			Activo = true;
		}
		//private static Empresa _empresa;
		//public static Empresa TraerGlobal()
		//{
		//	if( _empresa == null )
		//		_empresa = new Empresa();
		//	return _empresa;
		//}
	//	public static void Terminar() => _empresa = null;
		public static readonly Empresa Global = new Empresa();
	}

	class MiErrorException : Exception
	{
		public string Original { get; set; }
	}

	class DemoExcepciones
	{
		static int TraerEdad()
		{
			var texto = Console.ReadLine();
			if( texto.Contains( "3" ) ) {
				throw new MiErrorException() { Original = "Tiene un 3 :O " };
			}
			return int.Parse( texto );
		}
		static void Main( string[] args )
		{
			Console.WriteLine( "Hola estoy en MAIN" );
			
			var e = Empresa.Global;
			Console.WriteLine("Hola.. soy Alejandro");
			e.Usuario.Nombre = "Juan";
			Console.WriteLine("Hola ahora soy JUAN");
			Console.WriteLine(e.Usuario.Nombre);
			//try {
			//	var edad = TraerEdad();
			//	Console.WriteLine( $"La edad es {edad}" );
			//} catch( FormatException e ) {
			//	Console.WriteLine( $"E1 GT: {e.GetType().Name} M:{e.Message}" );
			//} catch( MiErrorException e ) {
			//	Console.WriteLine( $"E2 GT: {e.GetType().Name} M:{e.Original}" );
			//	throw new Exception( "Esto se esta complicando", e );
			//} catch( Exception e ) {
			//	Console.WriteLine( $"E2 GT: {e.GetType().Name} M:{e.Message}" );
			//}

			Console.ReadLine();
		}
	}
}
