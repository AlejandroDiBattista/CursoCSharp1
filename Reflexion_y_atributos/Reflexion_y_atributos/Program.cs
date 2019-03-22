namespace Reflexion_y_atributos {
    using System;
    using System.Reflection;

    // An enumeration of animals. Start at 1 (0 = uninitialized).
    public enum Animal { Dog = 1, Cat, Bird, }

    // A custom attribute to allow a target to have a pet.
    public class AnimalTypeAttribute : Attribute
    {
        // The constructor is called when the attribute is set.
        public AnimalTypeAttribute(Animal pet, string nombre)
        {
            this.Pet = pet;
            this.Nombre = nombre;
        }

        // .. and show a copy to the outside world.
        public Animal Pet { get; set; }
        public string Nombre { get; set; } = "Algo Lindo";

    }

    class AnimalTypeTestClass
    {
        [AnimalType(Animal.Dog,"Nubi")]
        public void DogMethod() {
            Console.WriteLine("GUAU");
        }

        [AnimalType(Animal.Cat, "ICat")]
        public void CatMethod() {
            Console.WriteLine("MIAU");
        }

        [AnimalType(Animal.Bird, "Loro")]
        public void BirdMethod() {
            Console.WriteLine("");
        }
    }

    class DemoClass
    {
        static void Main(string[] args)
        {
            AnimalTypeTestClass testClass = new AnimalTypeTestClass();
            Type type = testClass.GetType();
            foreach (MethodInfo mInfo in type.GetMethods())
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                {
                    mInfo.Invoke(testClass, null );
                    var m = (AnimalTypeAttribute)attr;
                    if (attr.GetType() == typeof(AnimalTypeAttribute))
                        Console.WriteLine( "Method [{0}] has a pet [{1}] attribute.", mInfo.Name, m.Nombre );
                }
            }
            Console.ReadLine();
        }
    }
}
