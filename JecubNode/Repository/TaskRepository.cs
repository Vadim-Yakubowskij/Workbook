namespace JecubNode.Repository
{
    public class TaskRepository
    {
        public static readonly HttpClient client = new HttpClient();
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
    }
}
