using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace qMax
{
    public class TorrentInfo
    {
        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("num_complete")]
        public int NumSeeds { get; set; } //Specifically, this is the number of seeds in the swarm, not the number of leechers the local user is seeding to
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            //Example API
            // /api/v2/torrents/info?filter=downloading&category=sample%20category&sort=ratio

            string url = "http://localhost:8080/api/v2/torrents/info";

            string apiResponse = await ApiCall(url);

            if (apiResponse != null)
            {
                var torrents = JsonSerializer.Deserialize<List<TorrentInfo>>(apiResponse);

                foreach (var t in torrents)
                {
                    Console.WriteLine($"ID: {t.Hash}");
                    Console.WriteLine($"Name: {t.Name}");
                    Console.WriteLine($"State: {t.State}");
                    Console.WriteLine($"Seeders: {t.NumSeeds}");
                    Console.WriteLine();
                }
            }

            url = "http://localhost:8080/api/v2/torrents/pause?heashes=all";


        }

        public static async Task<string> ApiCall(string url)
        {
            string apiResponse = null;

            using HttpClient client = new HttpClient();

            try
            {
                Console.WriteLine("Calling API...");

                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                apiResponse = await response.Content.ReadAsStringAsync();
                return apiResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
            }

            if (apiResponse == null)
            {
                Console.WriteLine("No response received.");
            }

            return apiResponse;
        }
    }
}