using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopdemo1.Models
{
    public class Wishlist
    {
        [Key]
        public int WishlistId { get; set; }  // Mã danh sách yêu thích
        [ForeignKey("Profile")]
        public int CustomerId { get; set; }  // Mã khách hàng
        public Profile profile { get; set; }  // Liên kết với khách hàng
        [ForeignKey("Product")]
        public string ProductCode { get; set; }  // Mã sản phẩm
        public Product Product { get; set; }  // Liên kết với sản phẩm
    }
}
