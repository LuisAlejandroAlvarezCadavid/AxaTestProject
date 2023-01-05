using System.Reflection;

namespace AxaTestProject.Shared.Exeptions
{
    public class CityExistExeption: Exception    
    {
        public CityExistExeption() : base() { }

        public CityExistExeption(string message) : base(message) { }

        public CityExistExeption(string message, Exception innerException) : base(message, innerException) { }

        public CityExistExeption(string message, Type objectExeption) : base(message)
        {
            Console.WriteLine($"{message}     {objectExeption.GetTypeInfo().Name}    {objectExeption.GetTypeInfo().Namespace}");
        }

    }
}
