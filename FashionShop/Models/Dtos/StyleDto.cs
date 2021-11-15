using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models.Dtos
{
    public class CreateStyleDto
    {
        [Required]
        [StringLength(maximumLength: 400, ErrorMessage = "Name Is Too Long")]
        public string Name { get; set; }

        [Required]
       
        public int GenderId { get; set; }
        [Required]
       
        public int MaterialCategoryId { get; set; }
        
        public bool Active { get; set; } = true;

        public string Description { get; set; }



    }
    public class StyleDto: CreateStyleDto
    {
        public int Id { get; set; }

        
        public GenderDto Gender { get; set; }
       
        public MaterialCategoryDto MaterialCategory { get; set; }
        public IList<ProductDto> Products { get; set; }
    }
    public class UpdateStyleDto: CreateStyleDto
    {
        public IList<ProductDto> Products { get; set; }

    }
}