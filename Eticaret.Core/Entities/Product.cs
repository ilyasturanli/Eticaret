using System.ComponentModel.DataAnnotations;

namespace Eticaret.Core.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Ürün Adı")]
        public string Name { get; set; }
        [Display(Name = "Ürün Açıklaması")]
        public string? Description { get; set; }// Açıklama isteğe bağlı olarak null olabilir.
        [Display(Name = "Resmi")]
        public string? Image { get; set; }// Logo isteğe bağlı olarak null olabilir.
        [Display(Name = "Fiyatı")]
        public decimal Price { get; set; }
        [Display(Name = "Kodu")]
        public string? ProductCode { get; set; }
        [Display(Name = "Adeti")]
        public int Stock { get; set; }
        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; }
        [Display(Name = "Anasayfa?")]
        public bool IsHome { get; set; }
        [Display(Name = "Kategori")]
        public int? CategoryId { get; set; }// FK (Category tablosundaki Id'ye karşılık gelir)
        [Display(Name = "Kategori")]
        public Category? Category { get; set; }//Navigation property(ilişkili nesne)
        //Bu ürün hangi kategoriye ait?.. sorusunun cevabı için Category sınıfı ile ilişkilendirildi.
        [Display(Name = "Marka")]
        public int? BrandId { get; set; }
        [Display(Name = "Marka")]
        public Brand? Brand { get; set; }
        [Display(Name = "Sıra No")]
        public int OrderNo { get; set; }
        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
