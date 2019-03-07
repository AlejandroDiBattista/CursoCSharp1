using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Ejercicios18
{


	class DemoStream
	{
		const string Archivo = @"..\..\..\prueba.txt";

		static void Probar1()
		{
			var sw = new StreamWriter( Archivo );
			sw.WriteLine( "1. Hola Mundo!" );
			sw.Close();

			var sr = new StreamReader( Archivo );
			Console.WriteLine( sr.ReadLine() );
			sr.Close();
		}
		static void Probar2()
		{
			var fs = new FileStream( Archivo, FileMode.Create );
			var sw = new StreamWriter( fs );
			sw.WriteLine( "2. Hola Mundo!" );
			sw.Close();

			fs = new FileStream( Archivo, FileMode.Open );
			var sr = new StreamReader( fs );
			Console.WriteLine( sr.ReadLine() );
			sr.Close();
		}
		static void Probar3()
		{
			var fs = new FileStream( Archivo, FileMode.Create );
			var sw = new StreamWriter( fs );
			sw.WriteLine( "3. Hola Mundo!" );
			sw.Close();

			fs = new FileStream( Archivo, FileMode.Open );
			var sr = new StreamReader( fs );
			Console.WriteLine( sr.ReadLine() );
			sr.Close();
		}
		static void Probar4()
		{
			var ms = new MemoryStream( );
			var sw = new StreamWriter( ms );

			sw.WriteLine( "4. Hola Mundo!" );
			sw.Close();
			
			var tmp = ms.ToArray();

			ms = new MemoryStream(tmp);
			var sr = new StreamReader( ms );
			Console.WriteLine( sr.ReadLine() );
			sr.Close();
		}
		static void Probar5()
		{
			var sw = new StringWriter( );
			sw.WriteLine( "5. Hola Mundo!" );
			var tmp = sw.ToString();
			sw.Close();

			var sr = new StringReader( tmp );
			Console.WriteLine( sr.ReadLine() );
			sr.Close();
		}
		static void Main( string[] args )
		{


			Console.ReadLine();
		}
	}
}
