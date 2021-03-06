using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models
{
    public class Size
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool Active { get; set; } = true;

        public virtual IList<ProductSize> ProductSizes { get; set; }


    }
}