using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abby.Model
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [Range(1, 1000, ErrorMessage = "Price should be between $1 and $1000")]
        public double Price { get; set; }

        [Display(Name = "Category Type")]
        public int CategoryId { get; set; }
        [Display(Name = "Food Type")]
        public int FoodTypeId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        [ForeignKey("FoodTypeId")]
        public virtual FoodType FoodType { get; set; }
    }
}
