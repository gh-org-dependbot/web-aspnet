using System.ComponentModel.DataAnnotations;

namespace WebAspnet.Models
{
    public class TodoViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        
        public bool IsCompleted { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public string FormattedCreatedAt => CreatedAt.ToString("g");
    }
}
