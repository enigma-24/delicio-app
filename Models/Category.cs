using System.ComponentModel.DataAnnotations;

namespace delicio_app.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        
        [Display(Name="Category Name")]
        [Required]
        public string Name { get; set; }
    }
}