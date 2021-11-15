using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models
{
    public class Gender
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public virtual IList<Style> Styles { get; set; }


    }
}