using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace JecubNode.Repository
{
    public class TodoRepository
    {
        public static readonly HttpClient client = new HttpClient();
        public async Task<int> create(Todo todo)
        {
            client.BaseAddress = new Uri("http://localhost:5212");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "/Create", todo);
            response.EnsureSuccessStatusCode();

            int userResponse = 0;
            if (response.IsSuccessStatusCode)
            {
                userResponse = DataSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync())!;
            }
            return userResponse;
        }
        public async Task<int> read(Todo todo)
        {
            client.BaseAddress = new Uri("http://localhost:5212");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "/Read", todo);
            response.EnsureSuccessStatusCode();

            int userResponse = 0;
            if (response.IsSuccessStatusCode)
            {
                userResponse = DataSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync())!;
            }
            return userResponse;
        }
        public async Task<int> update(Todo todo)
        {
            client.BaseAddress = new Uri("http://localhost:5212");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "/Update", todo);
            response.EnsureSuccessStatusCode();

            int userResponse = 0;
            if (response.IsSuccessStatusCode)
            {
                userResponse = DataSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync())!;
            }
            return userResponse;
        }
        public async Task<int> delete(Todo todo)
        {
            client.BaseAddress = new Uri("http://localhost:5212");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "/Delete", todo);
            response.EnsureSuccessStatusCode();

            int userResponse = 0;
            if (response.IsSuccessStatusCode)
            {
                userResponse = DataSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync())!;
            }
            return userResponse;
        }
        public async Task<int> readByID(Todo todo)
        {
            client.BaseAddress = new Uri("http://localhost:5212");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "/ReadByID", todo);
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
