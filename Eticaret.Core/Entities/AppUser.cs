using System.ComponentModel.DataAnnotations;

namespace Eticaret.Core.Entities
{
    public class AppUser :IEntity
    {
        public int Id { get; set; }
        
        [Display(Name = "Adı")]
        [Required(ErrorMessage = "Ad alanı zorunludur")]
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir")]
        public string Name { get; set; }
        
        [Display(Name = "Soyadı")]
        [Required(ErrorMessage = "Soyad alanı zorunludur")]
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir")]
        public string Surname { get; set; }
        
        [Required(ErrorMessage = "Email alanı zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        [StringLength(50, ErrorMessage = "Email en fazla 50 karakter olabilir")]
        public string Email { get; set; }
        
        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "Telefon alanı zorunludur")]
        [StringLength(11, ErrorMessage = "Telefon en fazla 11 karakter olabilir")]
        public string Phone { get; set; }
        
        [Display(Name = "Şifre")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Şifre en az 6, en fazla 50 karakter olabilir")]
        public string? Password { get; set; }
        
        [Display(Name = "Kullanıcı Adı")]
        [StringLength(50, ErrorMessage = "Kullanıcı adı en fazla 50 karakter olabilir")]
        public string? UserName { get; set; }// Kullanıcı adı isteğe bağlı olarak null olabilir.
        
        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; }
        
        [Display(Name = "Admin?")]
        public bool IsAdmin { get; set; }
        
        [Display(Name = "Kayıt Tarihi"),ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        
        [ScaffoldColumn(false)]
        public Guid? UserGuid { get; set; } = Guid.NewGuid(); // Seed ve migration tutarlılığı için dinamik varsayılan kaldırıldı
        //Guid (Globally Unique Identifier) = dünya çapında benzersiz bir kimlik numarasıdır
        //Örneğin: 550e8400-e29b-41d4-a716-446655440000
        public List<Adress>? Address { get; set; }// bir kullanıcının birden fazla adresi olabilir...

    }

}
