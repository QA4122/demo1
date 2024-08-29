using Shopdemo1.Data;
using Shopdemo1.Models;

namespace Shopdemo1.Repository
{
    public interface IWishlistRepository
    {
        Wishlist GetById(int id);  // Lấy thông tin danh sách yêu thích theo mã
        List<Wishlist> GetByCustomerId(int customerId, FilterModel filter);  // Lấy danh sách yêu thích theo mã khách hàng
        bool AddWishlist(Wishlist wishlist);  // Thêm danh sách yêu thích mới
        bool DeleteWishlist(int id);  // Xóa danh sách yêu thích theo mã
        bool Save();
    }
    public class WishlistRepository : IWishlistRepository
    {
        private readonly DataContext context;

        public WishlistRepository(DataContext context)
        {
            this.context = context;
        }
        public bool AddWishlist(Wishlist wishlist)
        {
            context.wishlists.Add(wishlist);
            return Save();
        }

        public bool DeleteWishlist(int id)
        {
            var i = context.wishlists.Where(c=>c.WishlistId == id).FirstOrDefault();
            context.wishlists.Remove(i);
            return Save();
        }

        public List<Wishlist> GetByCustomerId(int customerId, FilterModel filter)
        {
            return context.wishlists.Where(c=>c.CustomerId == customerId).Skip((filter.PageNumber - 1) * filter.PageSize)
        .Take(filter.PageSize).ToList();
        }

        public Wishlist GetById(int id)
        {
            return context.wishlists.Where(c => c.WishlistId == id).FirstOrDefault();
        }

        public bool Save()
        {
            var c = context.SaveChanges();
            return c >0 ? true : false;
        }
    }
}
