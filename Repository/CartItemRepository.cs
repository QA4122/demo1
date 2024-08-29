using Shopdemo1.Data;
using Shopdemo1.Models;

namespace Shopdemo1.Repository
{
    public interface ICartItemRepository
    {
        CartItem GetById(int id);  // Lấy thông tin sản phẩm trong giỏ hàng theo mã
        List<CartItem> GetByCartId(int cartId, FilterModel filter);  // Lấy danh sách sản phẩm trong giỏ hàng theo mã giỏ hàng
        bool AddCartItem(CartItem cartItem);  // Thêm sản phẩm mới vào giỏ hàng
        bool UpdateCartItem(CartItem cartItem);  // Cập nhật thông tin sản phẩm trong giỏ hàng
        bool DeleteCartItem(int id);  // Xóa sản phẩm trong giỏ hàng theo mã
        bool save();
    }
    public class CartItemRepository : ICartItemRepository
    {
        private readonly DataContext context;

        public CartItemRepository(DataContext context)
        {
            this.context = context;
        }

        public bool AddCartItem(CartItem cartItem)
        {
            context.cartItems.Add(cartItem);
            return save();
        }

        public bool DeleteCartItem(int id)
        {
            var con = context.cartItems.Where(c=>c.CartItemId == id).FirstOrDefault();
            context.cartItems.Remove(con);
            return save();
        }

        public List<CartItem> GetByCartId(int cartId, FilterModel filter)
        {
            return context.cartItems.Where(c=>c.CartId == cartId).Skip((filter.PageNumber - 1) * filter.PageSize)
        .Take(filter.PageSize).ToList();
        }

        public CartItem GetById(int id)
        {
            return context.cartItems.Where(c => c.CartId == id).FirstOrDefault();
        }

        public bool save()
        {
            var save = context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateCartItem(CartItem cartItem)
        {
            context.cartItems.Update(cartItem);
            return save();
        }
    }
}
