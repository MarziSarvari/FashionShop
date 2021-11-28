using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FashionShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StyleId { get; set; }

        [ForeignKey("StyleId")]
        public Style Style { get; set; }

        public string Description { get; set; } 
        
       
        [Required]
        public int ColorId { get; set; }

        [ForeignKey("ColorId")]
        public Color Color { get; set; }

        public bool Active { get; set; } = true;

        public string ImageUrl { get; set; }

        public virtual IList<ProductSize> ProductSizes { get; set; }





    }
}
