using Eticaret.Core.Entities;

namespace Eticaret.WebUI.Models
{
    public class HomePageViewModel
    {
        public List<Slider>? Sliders { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public List<News> News { get; set; } = new List<News>();
    }
}
