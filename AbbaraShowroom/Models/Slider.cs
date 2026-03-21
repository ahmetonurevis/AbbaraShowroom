using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AbbaraShowroom.Models
{
    public class Slider
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Buton Metni")]
        public string ButtonText { get; set; }

        [Display(Name = "Buton Linki")]
        public string ButtonUrl { get; set; }

        [Display(Name = "Görsel Yolu")]
        [ValidateNever] // ✅ Navigation property validasyondan çıkarıldı
        public string ImagePath { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
