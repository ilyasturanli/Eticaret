using System.ComponentModel.DataAnnotations;

namespace Eticaret.Core.Entities
{
    public class Category:IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; }
        [Display(Name = "Kategori Açıklaması")]
        public string? Description { get; set; }// Açıklama isteğe bağlı olarak null olabilir.
        [Display(Name = "Kategori Resimi")]
        public string? Image { get; set; }// Logo isteğe bağlı olarak null olabilir.
        [Display(Name = "Aktif Kategori Mi?")]
        public bool IsActive { get; set; }
        [Display(Name = "Üst Menüde Göster")]
        public bool IsTopMenu { get; set; }//Bu kategorinin ana menüde görünüp görünmeyeceğini belirler.
        [Display(Name = "Üst Kategori")]
        public int ParentId { get; set; }//Amacı: Alt kategoriler ile üst kategoriler arasındaki bağlantıyı kurmak.
        [Display(Name = "Sıra No")]
        public int OrderNo { get; set; }
        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public IList<Product>? Products { get; set; }// O kategoriye ait tüm ürünler listelemek icin...
        //Category ile Product arasındaki bire-çok (one-to-many) ilişkiyi temsil ediyor.
        //Yani “Bu kategoriye bağlı ürünlerin listesi” bilgisini tutuyor.

    }
}
