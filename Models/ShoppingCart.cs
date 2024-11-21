using First_Project.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace First_Project.Models
{
    public class ShoppingCart
    {
        private readonly FirstProjectDBContext _context;

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(FirstProjectDBContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            var context = services.GetRequiredService<FirstProjectDBContext>();
            var session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Produit produit, int quantity)
        {
            var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
                s => s.ProduitId == produit.IdProduit && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    ProduitId = produit.IdProduit,
                    Quantity = 1
                };

                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Quantity += quantity;
            }

            _context.SaveChanges();
        }

        public void RemoveFromCart(Produit produit)
        {
            var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
                s => s.ProduitId == produit.IdProduit && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Quantity > 1)
                {
                    shoppingCartItem.Quantity--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }

                _context.SaveChanges();
            }
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems = _context.ShoppingCartItems
                       .Where(c => c.ShoppingCartId == ShoppingCartId)
                       .Include(s => s.Produit)
                       .ToList());
        }
   
        public void ClearCart()
        {
            var cartItems = _context.ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _context.ShoppingCartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            return _context.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Produit.prix * c.Quantity)
                .Sum();
        }

     
    }
}

