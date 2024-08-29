using System.ComponentModel.DataAnnotations;

namespace Shopdemo1.Models
{
    public class Account
    {
        [Key]
        public int? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
    }
}
