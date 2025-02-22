using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruckApp.Models
{
    public class Receipt
    {
        [Key]
        public int Receipt_ID { get; set; }

        [Required, ForeignKey("Customer")]
        public int Customer_ID { get; set; }

        public virtual Customer? Customer { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Purchase>? Purchases { get; set; } = new List<Purchase>();
    }
}
