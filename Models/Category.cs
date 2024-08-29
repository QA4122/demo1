using System.ComponentModel.DataAnnotations;

namespace Shopdemo1.Models
{
    public class Category
    {
        [Key]
        public int? CategoryId { get; set; }  // Mã danh mục
        public string CategoryName { get; set; }  // Tên danh mục
        public string Description { get; set; }  // Mô tả danh mục
        public List<Product> Products { get; set; }  // Danh sách sản phẩm trong danh mục
    }
}
