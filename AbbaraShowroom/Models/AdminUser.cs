using System.ComponentModel.DataAnnotations;

namespace AbbaraShowroom.Models
{
    public class AdminUser
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}
