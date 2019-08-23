using System;
using System.Collections.Generic;
using System.Text;

namespace Prueba2 {

    public abstract class Padre {
        public string A { get; set; }
        public string B { get; set; }
        

        protected bool Equals(Padre other)
        {
            return Equals(A, other?.A) && object.Equals(B, other?.B);
        }

        public override bool Equals(object obj) {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;
            if(obj.GetType() != GetType()) return false;
            return Equals(obj as Padre);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (A != null ? A.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (B != null ? B.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
    
    public class Hijo : Padre { }

    public class Hija : Padre, IEquatable<Hija> {
        public bool Equals(Hija other) {
            return Equals(A, other?.A) && object.Equals(B, other?.B);
        }
        
    }
    
    public class Nieto : Hijo { }

    public class Nieta : Padre, IEquatable<Nieta> {
        public bool Equals(Nieta other) {
            return Equals(A, other?.A) && object.Equals(B, other?.B);
        }
    }
    
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" » Prueba herencia en abstract");

            var a = new Hija() { A = "Hola", B = "Chau" };
            object b = new Hija() { A = "Hola", B = "Chau" };

            Console.WriteLine($" · a.Equals(b)    => {a.Equals(b)}");
            Console.WriteLine($" · b.Equals(a)    => {b.Equals(a)}");
            Console.WriteLine($" · a.Equals(null) => {a.Equals(null)}");

            Console.ReadLine();
        }
    }
}
