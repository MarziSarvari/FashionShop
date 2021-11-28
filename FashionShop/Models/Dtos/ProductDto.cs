using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models.Dtos
{
    public class CreateProductDto
    {
        [Required]
        public int StyleId { get; set; }
        public string Description { get; set; }
        
        [Required]
        public int ColorId { get; set; }
        public bool Active { get; set; } = true;
        public string ImageUrl { get; set; }

    }
    public class ProductDto: CreateProductDto
    {
        public int Id { get; set; }

        public StyleDto Style { get; set; }
       
        public ColorDto Color { get; set; }
      
        public IList<ProductSizeDto> ProductSizes { get; set; }
    }
    public class UpdateProductDto: CreateProductDto
    {
        public IList<ProductSizeDto> ProductSizes { get; set; }

    }
}