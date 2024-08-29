using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopdemo1.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }  // Mã sản phẩm trong giỏ hàng
        [ForeignKey("Cart")]
        public int CartId { get; set; }  // Mã giỏ hàng
        public Cart Cart { get; set; }  // Liên kết với giỏ hàng
        [ForeignKey("Product")]
        public string ProductCode { get; set; }  // Mã sản phẩm
        public Product Product { get; set; }  // Liên kết với sản phẩm
        public int Quantity { get; set; }  // Số lượng sản phẩm
    }
}
