using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class ApplicationParameter
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? Value { get; set; }
    }
}
