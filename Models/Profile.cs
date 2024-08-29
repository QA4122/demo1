using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopdemo1.Models
{
    public class Profile
    {
        [Key]
        public int CustomerId { get; set; }  // Mã khách hàng
        public string FullName { get; set; }  // Họ và tên khách hàng
        public string PhoneNumber { get; set; }  // Số điện thoại khách hàng
        public string Address { get; set; }  // Địa chỉ khách hàng
        public DateTime DateOfBirth { get; set; }  // Ngày sinh khách hàng
        public List<Order> Orders { get; set; }  // Danh sách đơn hàng của khách hàng
        public List<CartItem> CartItems { get; set; }  // Danh sách sản phẩm trong giỏ hàng
        public List<Wishlist> Wishlists { get; set; }  // Danh sách yêu thích của khách hàng
        [ForeignKey("Account")]
        public int? AccountId { get; set; }
        public Account? account { get; set; }
    }
}
