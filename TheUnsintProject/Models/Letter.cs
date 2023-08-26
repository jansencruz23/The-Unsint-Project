using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace TheUnsintProject.Models
{
    public class Letter
    {
        public int Id { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Message { get; set; }
        public LetterColor Color { get; set; } = LetterColor.WHITE;

        [DataType(DataType.Date)]
        public DateTime? DatePosted { get; set; } = DateTime.Now;

        public string ColorLabel => 
            (Color.ToString()
                .Substring(0, 1)
                .ToUpper() +
            Color.ToString()
                .Substring(1)
                .ToLower())
            .Replace("_", " ");
    }
}