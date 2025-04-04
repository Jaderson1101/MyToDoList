using System.ComponentModel.DataAnnotations;

namespace MyToDoList.Models
{
    public class TodoTask
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        [Required]
        public string UserId { get; set; } // Simulando usuário
    }
}
