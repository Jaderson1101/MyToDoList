namespace MyToDoList.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public DateTime? ReminderDate { get; set; }

        public int TaskListId { get; set; }
        public TaskList TaskList { get; set; }
    }
}
