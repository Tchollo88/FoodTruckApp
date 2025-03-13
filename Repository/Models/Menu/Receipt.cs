using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models.Menu
{
    public class Receipt
    {
        [Key]
        public int Receipt_ID { get; set; }

        [Required, ForeignKey("Order")]
        public int Order_ID { get; set; }
        public virtual Order? Order { get; set; }

        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice
        {
            get
            {
                return Order?.SubTotal ?? 0;
            }
        }

        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
