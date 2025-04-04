namespace MyToDoList.Models
{
    public class TaskList
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User User { get; set; }

        public List<TaskItem> Tasks { get; set; } = new();
    }
}
