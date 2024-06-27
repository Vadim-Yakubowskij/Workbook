namespace JecubNode
{
    public class Todo
    {
        public Todo(Todo x)
        {
            Id = x.Id;
            Name = x.Name;  
            Date_time = x.Date_time;
            More_details = x.More_details;
            User_Id = x.User_Id;
        }

        public int Id { get; set; }
        public string Date_time { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string More_details { get; set; } = string.Empty;
        public int User_Id { get; set; }

    }
}

