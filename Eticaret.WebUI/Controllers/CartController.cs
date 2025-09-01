using Eticaret.Core.Entities;
using Eticaret.Service.Abstract;
using Eticaret.Service.Concrete;
using Eticaret.WebUI.ExtensionMethods;
using Eticaret.WebUI.Models;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Eticaret.WebUI.Controllers
{
    
    public class CartController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IService<Product> _serviceProduct;
        private readonly IService<AppUser> _serviceAppUser;
        private readonly IService<Adress> _addressService;
        private readonly IOrderService _orderService;

        public CartController(
            IService<Product> serviceProduct,
            IService<AppUser> serviceAppUser,
            IService<Adress> addressService,
            IOrderService orderService,
            IConfiguration config)
        {
            _config = config;
            _serviceProduct = serviceProduct;
            _serviceAppUser = serviceAppUser;
            _addressService = addressService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            var model = new CartViewModel()
            {
                CartLines = cart.CartLines,
                TotalPrice = cart.GetTotalPrice(),
            };
            return View(model);
        }

        public IActionResult Add(int productId, int quantity = 1)
        {
            var product = _serviceProduct.Find(productId);
            if (product != null)
            {
                var cart = GetCart();
                cart.AddProduct(product, quantity);
                SaveCart(cart);
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return RedirectToAction("Index");
        }

        public IActionResult Update(int productId, int quantity = 1)
        {
            var product = _serviceProduct.Find(productId);
            if (product != null)
            {
                var cart = GetCart();
                cart.UpdateProductQuantity(product, quantity);
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int productId)
        {
            var product = _serviceProduct.Find(productId);
            if (product != null)
            {
                var cart = GetCart();
                cart.RemoveProduct(product);
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var cart = GetCart();
            if (!cart.CartLines.Any())
                return RedirectToAction("Index");

            var userGuidClaim = HttpContext.User.FindFirst("UserGuid");
            var appuser = await _serviceAppUser.GetAsync(x => x.UserGuid.ToString() == userGuidClaim.Value);
            if (appuser == null)
                return RedirectToAction("SignIn", "Account");

            var addresses = await _addressService.GetAllAsync(x => x.AppUserId == appuser.Id && x.IsActive && x.IsBillingAdress && x.IsDeliveryAdress);

            var model = new CheckoutViewModel()
            {
                CartProducts = cart.CartLines,
                TotalPrice = cart.GetTotalPrice(),
                Addresses = addresses
            };
            return View(model);
        }

        public async Task<IActionResult> OrderSuccess(int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderWithDetailsAsync(orderId);
                if (order == null) return RedirectToAction("Index", "Home");
                return View(order);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Clear()
        {
            var cart = GetCart();
            cart.ClearCart();
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetCartCount()
        {
            var cart = GetCart();
            var count = cart.CartLines.Sum(x => x.Quantity);
            return Json(new { count = count });
        }

        private CartService GetCart()
        {
            return HttpContext.Session.GetJson<CartService>("Cart") ?? new CartService();
        }

        private void SaveCart(CartService cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }


        // Iyzipay ödeme entegrasyonu
        public async Task<IActionResult> Payment(int shippingAddressId, int billingAddressId)
        {
            var cart = GetCart();
            if (!cart.CartLines.Any()) return RedirectToAction("Index");

            try
            {
                var options = new Options
                {
                    ApiKey = _config["Iyzipay:ApiKey"],
                    SecretKey = _config["Iyzipay:SecretKey"],
                    BaseUrl = _config["Iyzipay:BaseUrl"]
                };

                var cargo = cart.GetTotalPrice() >= 10000 ? 0 : 450;
                var totalPrice = cart.GetTotalPrice() + cargo;

                var request = new CreateCheckoutFormInitializeRequest
                {
                    Locale = Locale.TR.ToString(),
                    ConversationId = Guid.NewGuid().ToString(),
                    Price = totalPrice.ToString("F2", CultureInfo.InvariantCulture),
                    PaidPrice = totalPrice.ToString("F2", CultureInfo.InvariantCulture),
                    Currency = Currency.TRY.ToString(),
                    BasketId = Guid.NewGuid().ToString(),
                    PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                    CallbackUrl = Url.Action("Callback", "Cart", null, Request.Scheme)
                };

                request.BasketItems = cart.CartLines.Select(line => new BasketItem
                {
                    Id = line.Product.Id.ToString(),
                    Name = line.Product.Name,
                    Category1 = "Genel",
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Price = (line.Product.Price * line.Quantity).ToString("F2", CultureInfo.InvariantCulture),
                }).ToList();

                if (cargo > 0)
                {
                    request.BasketItems.Add(new BasketItem
                    {
                        Id = "Kargo Bedeli",
                        Name = "Kargo Ücreti",
                        Category1 = "Hizmet",
                        ItemType = BasketItemType.PHYSICAL.ToString(),
                        Price = cargo.ToString("F2", CultureInfo.InvariantCulture),
                    });
                }

                var userGuidClaim = HttpContext.User.FindFirst("UserGuid");
                var appuser = await _serviceAppUser.GetAsync(x => x.UserGuid.ToString() == userGuidClaim.Value);

                request.Buyer = new Buyer
                {
                    Id = appuser.Id.ToString(),
                    Name = appuser.Name,
                    Surname = appuser.Surname,
                    Email = appuser.Email,
                    IdentityNumber = "TC YOK",
                    RegistrationAddress = appuser.Address?.FirstOrDefault()?.OpenAdress ?? "Adres yok",
                    City = appuser.Address?.FirstOrDefault()?.City ?? "şehir yok",
                    Country = "Turkey",
                    Ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString()
                };

                request.ShippingAddress = new Iyzipay.Model.Address
                {
                    ContactName = appuser.Name + " " + appuser.Surname,
                    City = appuser.Address?.FirstOrDefault()?.City ?? "Şehir bilgisi yok",
                    Country = "Turkey",
                    Description = appuser.Address?.FirstOrDefault()?.OpenAdress ?? "Adresi Yok"
                };

                request.BillingAddress = new Iyzipay.Model.Address
                {
                    ContactName = appuser.Name + " " + appuser.Surname,
                    City = appuser.Address?.FirstOrDefault()?.City ?? "Şehir bilgisi yok",
                    Country = "Turkey",
                    Description = appuser.Address?.FirstOrDefault()?.OpenAdress ?? "Adresi Yok"
                };

                var checkoutFormInitialize = await CheckoutFormInitialize.Create(request, options);



                if (checkoutFormInitialize.Status == "success")
                {
                    ViewBag.CheckoutFormContent = checkoutFormInitialize.CheckoutFormContent;
                    return View("Payment");
                }
                else
                {
                    TempData["ErrorMessage"] = $"Ödeme formu oluşturulamadı. Hata: {checkoutFormInitialize.ErrorMessage}";
                    return RedirectToAction("Checkout");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ödeme sistemi hatası: " + ex.Message;
                return RedirectToAction("Checkout");
            }
        }

        [Authorize,HttpPost]
        public async Task<IActionResult> Callback(string token)
        {
            try
            {
                var options = new Options
                {
                    ApiKey = _config["Iyzipay:ApiKey"],
                    SecretKey = _config["Iyzipay:SecretKey"],
                    BaseUrl = _config["Iyzipay:BaseUrl"]
                };

                var request = new RetrieveCheckoutFormRequest { Token = token };
                var checkoutForm = await CheckoutForm.Retrieve(request, options);

                if (checkoutForm.Status.ToLower() == "success" && checkoutForm.PaymentStatus.ToLower() == "success")
                {
                    var userGuidClaim = HttpContext.User.FindFirst("UserGuid");

                    var appuser = await _serviceAppUser.GetAsync(x => x.UserGuid.ToString() == userGuidClaim.Value);
                    //if (appuser == null)
                    //    return RedirectToAction("SignIn", "Account");

                    // Adresleri ayrı çekiyoruz ve C# tarafında işliyoruz
                    var addresses = await _addressService.GetAllAsync(x => x.AppUserId == appuser.Id && x.IsActive && x.IsDeliveryAdress && x.IsBillingAdress);
                    var deliveryAddress = addresses.FirstOrDefault(a => a.IsDeliveryAdress)?.OpenAdress ?? "Teslimat Adresi";
                    var billingAddress = addresses.FirstOrDefault(a => a.IsBillingAdress)?.OpenAdress ?? "Fatura Adresi";

                    var cart = GetCart();

                    var order = await _orderService.CreateOrderAsync(
                        appuser.Id,
                        cart.CartLines,
                        deliveryAddress: deliveryAddress,
                        billingAddress: billingAddress
                    );

                    // Sepeti temizle
                    cart.ClearCart();
                    SaveCart(cart);

                    return RedirectToAction("OrderSuccess", new { orderId = order.Id });
                }
                else
                {
                    TempData["ErrorMessage"] = "Ödeme başarısız: " + (checkoutForm.ErrorMessage ?? "Bilinmeyen hata");
                    return RedirectToAction("Fail");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ödeme sırasında hata: " + ex.Message;
                return RedirectToAction("Fail");
            }
        }


        public IActionResult Fail()
        {
            return View();
        }
    }
}
