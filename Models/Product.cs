using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopdemo1.Models
{
    public class Product
    {
        [Key]
        public string ProductCode { get; set; }  // Mã sản phẩm duy nhất
        public string ProductName { get; set; }  // Tên sản phẩm
        public string Description { get; set; }  // Mô tả sản phẩm
        public decimal Price { get; set; }  // Giá sản phẩm
        public int Stock { get; set; }  // Số lượng sản phẩm trong kho
        public int WarnAmount { get; set; }  // Mức cảnh báo số lượng tồn kho
        public DateTime CreateDate { get; set; }  // Ngày tạo sản phẩm
        [ForeignKey("Category")]
        public int CategoryId { get; set; }  // Mã danh mục sản phẩm
        public Category Category { get; set; }  // Liên kết với danh mục
        
    }
}
