namespace JecubNode.Repository;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using JecubNode;

public class UserRepository
{
    public  readonly HttpClient client = new HttpClient();



    public  async Task<User> GetUserAsync(string path)
    {
        client.BaseAddress = new Uri("http://localhost:5212");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        User user = null;


        HttpResponseMessage response = await client.GetAsync(path);
        if (response.IsSuccessStatusCode)
        {
            user = DataSerializer.Deserialize<User>(await response.Content.ReadAsStringAsync())!;
        }
        return user;
    }

    public  async Task<int> Register(User user)
    {
        client.BaseAddress = new Uri("http://localhost:5212");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage response = await client.PostAsJsonAsync(
            "/register", user);
        response.EnsureSuccessStatusCode();

        int userResponse = 0;
        if (response.IsSuccessStatusCode)
        {
            userResponse = DataSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync())!;
        }
        return userResponse;
    }

    public  async Task<int> Login(User user)
    {
        client.BaseAddress = new Uri("http://localhost:5212");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage response = await client.PostAsJsonAsync(
            "/login", user);
        response.EnsureSuccessStatusCode();

        int userResponse = 0;
        if (response.IsSuccessStatusCode)
        {
            userResponse = DataSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync())!;
        }
        return userResponse;
    }
}
