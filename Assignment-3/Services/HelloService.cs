using Assignment_3.Services.IServices;

namespace Assignment_3.Services
{
    public class HelloService : IHelloSerivice
    {
        public string GetMessage()
        {
            return "Hello World!";
        }
    }
}
