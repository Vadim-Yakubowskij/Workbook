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
            int TodoId = await PostTodo(todo);

            await Console.Out.WriteLineAsync("Post");
            await Console.Out.WriteLineAsync(Convert.ToString(TodoId));

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        Console.ReadLine();
    }
         public static async Task<Todo> GetTaskAsync(string path)
         {
                Todo todo = null;


                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    todo = DataSerializer.Deserialize<Todo>(await response.Content.ReadAsStringAsync());
                }
                return todo;
         }

            public static async Task<List<Todo>> GetTodo()
            {

                HttpResponseMessage response = await client.GetAsync(
                    "/Read");
                response.EnsureSuccessStatusCode();

                List<Todo> TodoResponse = new List<Todo>();
                if (response.IsSuccessStatusCode)
                {
                    TodoResponse = DataSerializer.Deserialize<List<Todo>>(await response.Content.ReadAsStringAsync());
                }
                return TodoResponse;
            }

            public static async Task<int> PostTodo(Todo todo)
            {

                HttpResponseMessage response = await client.PostAsync(
                    "/Create", JsonContent.Create(todo));
                response.EnsureSuccessStatusCode();

                int SongResponse = 0;
                if (response.IsSuccessStatusCode)
                {
                    SongResponse = DataSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync());
                }
                return SongResponse;
            }

    
}

