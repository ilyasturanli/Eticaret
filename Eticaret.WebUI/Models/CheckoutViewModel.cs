﻿using Eticaret.Core.Entities;

namespace Eticaret.WebUI.Models
{
    public class CheckoutViewModel
    {
        public List<CartLine> CartProducts { get; set; }
        public decimal TotalPrice { get; set; }
        public List<Adress>? Addresses { get; set; }

    }
}
