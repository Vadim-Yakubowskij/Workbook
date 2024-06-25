using System.Net.Http;
using System.Net.Http.Json;

namespace JecubNode.Repository
{
    public class TaskRepository
    {
        public static readonly HttpClient client = new HttpClient();
        static async Task<int> Create(User user)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "/Create", user);
            response.EnsureSuccessStatusCode();

            int userResponse = 0;
            if (response.IsSuccessStatusCode)
            {
                userResponse = DataSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync())!;
            }
            return userResponse;
        }
        static async Task<int> Read(User user)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "/Read", user);
            response.EnsureSuccessStatusCode();

            int userResponse = 0;
            if (response.IsSuccessStatusCode)
            {
                userResponse = DataSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync())!;
            }
            return userResponse;
        }
        static async Task<int> Update(User user)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "/Update", user);
            response.EnsureSuccessStatusCode();

            int userResponse = 0;
            if (response.IsSuccessStatusCode)
            {
                userResponse = DataSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync())!;
            }
            return userResponse;
        }
        static async Task<int> Delete(User user)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "/Delete", user);
            response.EnsureSuccessStatusCode();

            int userResponse = 0;
            if (response.IsSuccessStatusCode)
            {
                userResponse = DataSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync())!;
            }
            return userResponse;
        }
        static async Task<int> ReadByID(User user)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "/ReadByID", user);
            response.EnsureSuccessStatusCode();

            int userResponse = 0;
            if (response.IsSuccessStatusCode)
            {
                userResponse = DataSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync())!;
            }
            return userResponse;
        }
    }
}
