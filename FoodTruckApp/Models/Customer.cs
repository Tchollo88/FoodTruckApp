using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodTruckApp.Models
{
    public class Customer
    {
        [Key]
        public int Customer_ID { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required, Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        public virtual ICollection<Receipt>? Receipts { get; set; } = new List<Receipt>();
    }
}
