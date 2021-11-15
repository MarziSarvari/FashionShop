using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FashionShop.Models.Dtos
{
    public class GenderDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public IList<StyleDto> Styles { get; set; }

    }
}
