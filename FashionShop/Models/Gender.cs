using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models
{
    public class Gender
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}