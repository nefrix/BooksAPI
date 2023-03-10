using System.ComponentModel.DataAnnotations;

namespace Smd.InterviewAssignment.WebApi.Models
{
    public class Book
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public bool IsRead { get; set; }

        public override string ToString()
        {
            return $"{Author} - {Title}";
        }
    }
}
