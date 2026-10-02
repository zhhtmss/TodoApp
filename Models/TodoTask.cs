using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class TodoTask
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введіть назву завдання")]
        [StringLength(100)]
        public string Title { get; set; } = "";

        public bool IsCompleted { get; set; }
    }
}
