using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Core.Entities
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        [Display(Name ="Siparis No"),StringLength(50)]
        public string OrderNumber { get; set; }
        [Display(Name = "Siparis Toplamı")]
        public decimal TotalPrice { get; set; }
        [Display(Name = "Müsteri No")]
        public int AppUserId { get; set; }
        [Display(Name = "Müsteri")]
        public AppUser? AppUser { get; set; }
        [Display(Name = "Müsteri"), StringLength(50)]
        public string CustomerId { get; set; }
        [Display(Name = "Fatura Adresi"), StringLength(500)]
        public string BillingAddress { get; set; }
        [Display(Name = "Teslimat Adresi"), StringLength(500)]
        public string DeliveryAddress { get; set; }
        [Display(Name = "Siparis Tarihi")]
        public DateTime OrderDate { get; set; }
        public List<OrderLine>? OrderLines { get; set; }
        public EnumOrderState OrderState { get; set; }
    }
    public enum EnumOrderState
    {
        [Display(Name = "Onay Bekliyor...")]
        Waiting,//onay bekliyor
        [Display(Name = "Onaylandı")]
        Approved,//Onaylandı
        [Display(Name = "Kargoya Verildi")]
        Shipped,//Kargoya verildi
        [Display(Name = "Siparis Tamamlandı.")]
        Completed,//Tamamlandı      
        [Display(Name = "İptal Edildi")]
        Cancelled,//İptal Edildi.
    }
}
