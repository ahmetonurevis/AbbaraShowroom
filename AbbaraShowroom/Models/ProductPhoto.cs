using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbbaraShowroom.Models
{
    public class ProductPhoto
    {
        public int Id { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [Display(Name = "Ürün")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
