using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models.Dtos
{
    public class CreateProductSizeDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int SizeId { get; set; }


    }
    public class ProductSizeDto: CreateProductSizeDto
    {
        public int Id { get; set; }

        public ProductDto Product { get; set; }

        public SizeDto Size { get; set; }
    }
    public class UpdateProductSizeDto: CreateProductSizeDto
    {

    }
}