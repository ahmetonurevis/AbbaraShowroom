using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AbbaraShowroom.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        [Display(Name = "Ürün Adı")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "Kategori seçilmelidir.")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [Display(Name = "Renk")]
        public string? Color { get; set; }

        [Display(Name = "Fiyat")]
        [Range(0, 1000000, ErrorMessage = "Geçerli bir fiyat giriniz.")]
        public decimal? Price { get; set; }

        [Display(Name = "Öne Çıkar")]
        public bool IsFeatured { get; set; }

        [Display(Name = "Eklenme Tarihi")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ValidateNever]
        public ICollection<ProductPhoto> Photos { get; set; }
    }
}
