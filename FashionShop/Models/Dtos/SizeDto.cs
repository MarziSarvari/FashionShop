using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FashionShop.Models.Dtos
{    
    public class CreateSizeDto
    {
        [Required]
        [StringLength(maximumLength: 200, ErrorMessage = "Name Is Too Long")]
        public string Name { get; set; }

        public bool Active { get; set; } = true;

    }
    public class SizeDto : CreateSizeDto
    {
        public int Id { get; set; }

        public IList<ProductSizeDto> ProductSizes { get; set; }
    }
    public class UpdateSizeDto : CreateSizeDto
    {
        public IList<ProductSizeDto> ProductSizes { get; set; }
    }

}
