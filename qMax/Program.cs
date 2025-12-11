using System;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace qMax
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string url = "http://localhost:8080/api/v2/app/version";

            using HttpClient client = new HttpClient();

            try
            {
                Console.WriteLine("Calling API...");

                HttpResponseMessage response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Response:");
                Console.WriteLine(responseBody);
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
            }
        }
    }
}