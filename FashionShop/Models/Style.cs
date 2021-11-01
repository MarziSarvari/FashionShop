using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionShop.Models
{
    public class Style
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int GenderId { get; set; }

        [ForeignKey("GenderId")]
        public Gender Gender { get; set; }
        [Required]
        public int MaterialCategoryId { get; set; }

        [ForeignKey("MaterialCategoryId")]
        public MaterialCategory MaterialCategory { get; set; }

        public bool Active { get; set; } = true;

        public string Description { get; set; }



    }
}