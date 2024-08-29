using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopdemo1.Models
{
    public class Shipment
    {
        [Key]
        public int ShipmentId { get; set; }  // Mã giao hàng
        public string ShipmentMethod { get; set; }  // Phương thức giao hàng (ví dụ: Giao hàng nhanh, Giao hàng thường)
        public DateTime ShipmentDate { get; set; }  // Ngày giao hàng
        public string TrackingNumber { get; set; }  // Mã vận đơn
        [ForeignKey("Profile")]
        public int customer_id { get; set; }
        public Profile profile { get; set; }
    }
}
