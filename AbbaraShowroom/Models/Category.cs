using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AbbaraShowroom.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı boş geçilemez.")]
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; }

        [ValidateNever] // ✅ Bu alan validasyona girmez
        public ICollection<Product> Products { get; set; }
    }
}
