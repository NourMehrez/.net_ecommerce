using First_Project.Models;
using First_Project.Models.repository.Interface;
using First_Project.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace First_Project.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProduitRepository _produitRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IProduitRepository produitRepository, ShoppingCart shoppingCart)
        {
            _produitRepository = produitRepository;
            _shoppingCart = shoppingCart;
        }

        // GET: SHoppingCartController
        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var sCVM = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCarteTotal = _shoppingCart.GetShoppingCartTotal(),
            };
            return View(sCVM);
        }

        public RedirectToActionResult AddToShoppingCart(int ProduitId)
        {
            var selectectedProduct = _produitRepository.Find(ProduitId);
            if (selectectedProduct != null)
            {
                _shoppingCart.AddToCart(selectectedProduct, 1);

            }
            return RedirectToAction("Index");
        }


        public RedirectToActionResult RemoveFromShoppingCart(int ProduitId)
        {
            var selectectedProduct = _produitRepository.Find(ProduitId);
            if (selectectedProduct != null)
            {
                _shoppingCart.RemoveFromCart(selectectedProduct);

            }
            return RedirectToAction("Index");
        }
    }
}
