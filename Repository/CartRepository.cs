using Shopdemo1.Data;
using Shopdemo1.Models;

namespace Shopdemo1.Repository
{
    public interface ICartREpository
    {
        Cart GetById(int id);  // Lấy thông tin giỏ hàng theo mã
        List<Cart> GetByCustomerId(int customerId, FilterModel filter);  // Lấy giỏ hàng theo mã khách hàng
        bool AddCart(Cart cart);  // Thêm giỏ hàng mới
        bool UpdateCart(Cart cart);  // Cập nhật thông tin giỏ hàng
        bool DeleteCart(int id);  // Xóa giỏ hàng theo mã
        bool save();
    }
    public class CartRepository : ICartREpository
    {
        private readonly DataContext context;

        public CartRepository(DataContext context)
        {
            this.context = context;
        }
        public bool AddCart(Cart cart)
        {
            context.carts.Add(cart);
            return save();
        }

        public bool DeleteCart(int id)
        {
            var a = context.carts.Where(c=>c.CartId == id).FirstOrDefault();
            context.carts.Remove(a);
            return save();
        }

        public List<Cart> GetByCustomerId(int customerId, FilterModel filter)
        {
            return context.carts.Where(c=>c.CustomerId==customerId).Skip((filter.PageNumber - 1) * filter.PageSize)
        .Take(filter.PageSize).ToList();
        }

        public Cart GetById(int id)
        {
            return context.carts.Where(c=>c.CartId==id).FirstOrDefault();
        }

        public bool save()
        {
            var save = context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateCart(Cart cart)
        {
            context.carts.Update(cart);
            return save();
        }
    }
}
