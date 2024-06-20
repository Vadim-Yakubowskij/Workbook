using System.Net.Http.Headers;
using System.Net.Http.Json;
using JecubNode;

internal class Program
{
    public static readonly HttpClient client = new HttpClient();

    private static async Task Main(string[] args)
    {
        client.BaseAddress = new Uri("http://localhost:5212");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
            // Create a new product
            Todo todo = new Todo
            {
                Id = 0,
                Date_time = "10.09.2024",
                Name = "Пока",
                More_details = "Привет"
            };

            // Get the product
            int TodoId = await Post(todo);

            await Console.Out.WriteLineAsync("Post");
            await Console.Out.WriteLineAsync(Convert.ToString(TodoId));

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        Console.ReadLine();
    }


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

    static async Task<int> Post(Todo todo)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync(
            "/Create", todo);
        response.EnsureSuccessStatusCode();

        int todoResponse = 0;
        if (response.IsSuccessStatusCode)
        {
            todoResponse = DataSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync())!;
        }
        return todoResponse;
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
