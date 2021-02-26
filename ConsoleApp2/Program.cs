using MyNamespace;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new AuthClient("https://localhost:5001/", new HttpClient());

            var request = new LoginRequest()
            {
                LastName = "Иванов"
            };

            var token = await client.LoginAsync(request);

            Console.WriteLine(token);
        }
    }
}
