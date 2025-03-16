using System.ComponentModel.DataAnnotations;

namespace WebAspnet.Models
{
    public class Todo
    {
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
