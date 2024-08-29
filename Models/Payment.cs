using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopdemo1.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }  // Mã thanh toán
        public string PaymentMethod { get; set; }  // Phương thức thanh toán (ví dụ: thẻ tín dụng, PayPal)
        public decimal Amount { get; set; }  // Số tiền thanh toán
        public DateTime PaymentDate { get; set; }  // Ngày thanh toán
        [ForeignKey("Profile")]
        public int customer_id { get; set; }
        public Profile profile { get; set; }
    }
}
