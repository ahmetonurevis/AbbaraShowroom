using System;
using System.ComponentModel.DataAnnotations;

namespace AbbaraShowroom.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Ad Soyad")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [StringLength(150)]
        [Display(Name = "Konu")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Mesaj")]
        public string Content { get; set; }
        public bool IsRead { get; set; } = false;

        [Display(Name = "Gönderim Tarihi")]
        public DateTime SentDate { get; set; } = DateTime.Now;
    }
}
