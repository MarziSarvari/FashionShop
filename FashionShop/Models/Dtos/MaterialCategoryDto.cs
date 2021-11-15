using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models.Dtos
{
    public class CreateMaterialCategoryDto
    {
        [Required]
        public int MaterialId { get; set; }
        [Required]
        public int CategoryId { get; set; }

    }
    public class MaterialCategoryDto:CreateMaterialCategoryDto
    {    
        public int Id { get; set; }
              
        public MaterialDto Material { get; set; }
        
        public CategoryDto Category { get; set; }

        public IList<StyleDto> Styles { get; set; }
    }
    public class UpdateMaterialCategoryDto : CreateMaterialCategoryDto
    {
        public IList<StyleDto> Styles { get; set; }
    }
}