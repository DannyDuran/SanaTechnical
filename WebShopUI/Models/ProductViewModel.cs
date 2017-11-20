using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopUI.Models
{
    [Table("Product")]
    public class ProductViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Number is required.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Number invalid.")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [Display(Name = "Title")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [RegularExpression(@"^\d{1,6}?$", ErrorMessage = "Format number invalid.")]
        public decimal Price { get; set; }
    }
}
