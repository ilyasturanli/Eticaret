using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Core.Entities
{
    public class Adress : IEntity
    {
        public int Id { get; set; }
        [Display(Name ="Adres Başlığı"),StringLength(50),Required(ErrorMessage = "{0} Alanı Zorunludur!")]
        public string Title { get; set; }
        [Display(Name = "Şehir"),StringLength(50), Required(ErrorMessage = "{0} Alanı Zorunludur!")]
        public string City { get; set; }//il
        [Display(Name = "İlçe"),StringLength(50), Required(ErrorMessage = "{0} Alanı Zorunludur!")]
        public string District { get; set; }//ilce
        [Display(Name = "Açık Adres"),DataType(DataType.MultilineText), Required(ErrorMessage = "{0} Alanı Zorunludur!")]
        public string OpenAdress { get; set; }
        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
        [Display(Name = "Fatura Adresi")]
        public bool IsBillingAdress { get; set; }//Fatura Adresi
        [Display(Name ="Teslimat Adresi")]
        public bool IsDeliveryAdress { get; set; }//Teslimat Adresi
        [Display(Name = "Fatura Tipi")]
        public string BillingType { get; set; } // "Bireysel" veya "Kurumsal"
        [Display(Name = "Ticari Ünvan")]
        public string? CompanyName { get; set; }
        [Display(Name = "Vergi No")]
        public string? TaxNumber { get; set; }
        [Display(Name = "Vergi Dairesi")]
        public string? TaxOffice { get; set; }
        [Display(Name ="Kayıt Tarihi"),ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [ScaffoldColumn(false)]
        public Guid? AdressGuid { get; set; } = Guid.NewGuid();
        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        

    }
}
