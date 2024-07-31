using System.ComponentModel.DataAnnotations;

namespace PROGETTO_S3.Models.ViewModel
{
    public class CreateProductViewModel
    {

        [Required]
        public Product Product { get; set; }

        public List<ingredients> Ingredients { get; set; } = new List<ingredients>();
        public List<int> SelectedIngredientIds { get; set; } = new List<int>();

        public IFormFile? Image { get; set; }
    }
}
