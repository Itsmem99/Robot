using Robot.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Robot.DAL
{
    public class MoodEntryApiClient
    {
        private readonly HttpClient _client;

        public MoodEntryApiClient(HttpClient httpClient)
        {
            _client = httpClient;
            _client.BaseAddress = new Uri("https://localhost:7207/");
        }

        public async Task CreateMoodEntryAsync(MoodEntry moodEntry)
        {
            var json = JsonSerializer.Serialize(moodEntry);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/MoodEntries", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
        }
    }
}
