using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Core.Entities
{
    public class Slider : IEntity // Slider, web sitesinde gösterilecek olan slaytları temsil eder.
    {
        public int Id { get; set; }
        [Display(Name = "Slider Başlığı")]
        public string? Title { get; set; }
        [Display(Name = "Slider Açıklaması")]
        public string? Description { get; set; }
        [Display(Name = "Slider Resimi")]
        public string? Image { get; set; }

        public string? Link { get; set; }

    }
}
