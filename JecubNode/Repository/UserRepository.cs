namespace JecubNode.Repository;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using JecubNode;

internal class Program
{
    public static readonly HttpClient client = new HttpClient();

    static async Task<User> GetUserAsync(string path)
    {
        User user = null;


        HttpResponseMessage response = await client.GetAsync(path);
        if (response.IsSuccessStatusCode)
        {
            user = DataSerializer.Deserialize<User>(await response.Content.ReadAsStringAsync())!;
        }
        return user;
    }

    static async Task<int> Register(User user)
    {
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

    static async Task<int> Login(User user)
    {

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
