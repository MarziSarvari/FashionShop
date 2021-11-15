using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FashionShop.Models.Dtos
{
    public class CreateMaterialDto
    {
        [Required]
        [StringLength(maximumLength: 200, ErrorMessage = "Name Is Too Long")]
        public string Name { get; set; }

        public bool Active { get; set; } = true;
    }
    public class MaterialDto:CreateMaterialDto
    {
        public int Id { get; set; }

        public IList<MaterialCategoryDto> MaterialCategories { get; set; }
    }
    public class UpdateMaterialDto : CreateMaterialDto
    {
        public IList<MaterialCategoryDto> MaterialCategories { get; set; }
    }

}
