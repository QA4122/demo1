using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopdemo1.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }  // Mã giỏ hàng
        [ForeignKey("Profile")]
        public int CustomerId { get; set; }  // Mã khách hàng
        public Profile Profile { get; set; }  // Liên kết với khách hàng
        public List<CartItem> CartItems { get; set; }  // Danh sách sản phẩm trong giỏ hàng
    }
}
