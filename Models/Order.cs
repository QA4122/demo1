using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopdemo1.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }  // Mã đơn hàng
        public DateTime OrderDate { get; set; }  // Ngày đặt hàng
        public decimal TotalAmount { get; set; }  // Tổng giá trị đơn hàng
        public string Status { get; set; }  // Trạng thái đơn hàng
        [ForeignKey("Profile")]
        public int? CustomerId { get; set; }  // Mã khách hàng
        public Profile? profile { get; set; }  // Liên kết với khách hàng
        public List<OrderItem> OrderItems { get; set; }  // Danh sách sản phẩm trong đơn hàng
        [ForeignKey("Shipment")]
        public int? ShipmentId { get; set; }  // Mã giao hàng
        public Shipment? Shipment { get; set; }  // Liên kết với thông tin giao hàng
        [ForeignKey("Payment")]
        public int? PaymentId { get; set; }
        public  Payment Payment { get; set; }
    }
}
