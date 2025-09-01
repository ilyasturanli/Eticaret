using System.ComponentModel.DataAnnotations;

namespace Eticaret.Core.Entities
{
    public class Brand : IEntity// marka //mesela örnek olarak bu bir apple yada samsung markası olabilir.
    {
        public int Id { get; set; }
        [Display(Name = "Marka Adı")]
        public string Name { get; set; }
        [Display(Name = "Marka Açıklaması")]
        public string? Description { get; set; }// Açıklama isteğe bağlı olarak null olabilir.
        [Display(Name = "Marka Logosu")]
        public string? Logo { get; set; }// Logo isteğe bağlı olarak null olabilir.
        [Display(Name = "Marka Aktif Mi?")]
        public bool IsActive { get; set; }
        [Display(Name = "Sıra No")]
        public int OrderNo { get; set; }// Sıralama numarası, markaların listelenmesinde kullanılabilir.
        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public IList<Product>? Products { get; set; }// bir markaya ait birden fazla ürün olabilir...
    }
}
