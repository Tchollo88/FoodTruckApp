using System.ComponentModel.DataAnnotations;

namespace FoodTruckApp.Models
{
    public class Account
    {
        [Key]
        public int Account_ID { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
