using System.ComponentModel.DataAnnotations;

namespace Eticaret.WebUI.Models
{
    public class UserEditViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Adı")]
        [Required(ErrorMessage = "Ad alanı zorunludur")]
        public string Name { get; set; }
        [Display(Name = "Soyadı")]
        [Required(ErrorMessage = "Soyad alanı zorunludur")]
        public string Surname { get; set; }
        [Display(Name = "E-posta")]
        [Required(ErrorMessage = "E-posta alanı zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; }
        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "Telefon alanı zorunludur")]
        public string? Phone { get; set; } // Telefon numarası isteğe bağlı olarak null olabilir.
        [Display(Name = "Şifre")]
        [DisplayFormat(NullDisplayText = "Değiştirmek için yeni şifre girin")]
        public string? Password { get; set; }
    }
}
