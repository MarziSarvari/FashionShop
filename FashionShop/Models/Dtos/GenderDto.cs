using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FashionShop.Models.Dtos
{
    public class GenderDto :CreateGenderDto
    {
        public int Id { get; set; }    
        public IList<StyleDto> Styles { get; set; }

    }
    public class CreateGenderDto
    {
        [Required]
        public string Name { get; set; }

    }
    public class UpdateGenderDto : CreateGenderDto
    {
        public IList<StyleDto> Styles { get; set; }

    }
}
