using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopdemo1.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }  // Mã chi tiết đơn hàng
        public int Quantity { get; set; }  // Số lượng sản phẩm
        public decimal UnitPrice { get; set; }  // Giá sản phẩm tại thời điểm đặt hàng
        [ForeignKey("Order")]
        public int OrderId { get; set; }  // Mã đơn hàng
        public Order Order { get; set; }  // Liên kết với đơn hàng
        [ForeignKey("Product")]
        public string ProductCode { get; set; }  // Mã sản phẩm
        public Product Product { get; set; }  // Liên kết với sản phẩm

    }
}
