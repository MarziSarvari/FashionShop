using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FashionShop.Models.Dtos
{
    public class CreateColorDto
    {      
        [Required]
        [StringLength(maximumLength: 200, ErrorMessage = "Name Is Too Long")]
        public string Name { get; set; }

        public bool Active { get; set; } = true;

    }
    public class ColorDto : CreateColorDto
    {
        public int Id { get; set; }

        public IList<ProductDto> Products { get; set; }
    }
    public class UpdateColorDto : CreateColorDto
    {
        public IList<ProductDto> Products { get; set; }
    }

}
