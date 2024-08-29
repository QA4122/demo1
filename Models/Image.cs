using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shopdemo1.Models
{
    public class Image
    {
        [Key]
        public int? Id { get; set; }
        public string ImageName { get; set; }
        public DateTime CreateDate { get; set; }
        [ForeignKey("Product")]
        public string ProductCode { get; set; }
        public Product Product { get; set; }
    }
}
