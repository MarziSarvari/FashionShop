using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models
{
    public class Color

    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool Active { get; set; } = true;

        
    }
}