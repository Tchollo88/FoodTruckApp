using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodTruckApp.Models.Client;
using FoodTruckApp.Models.Menu;

namespace FoodTruckApp.Models.Menu
{
    public class Receipt
    {
        [Key]
        public int Receipt_ID { get; set; }

        [Required, ForeignKey("Customer")]
        public int Customer_ID { get; set; }

        public virtual Customer? Customer { get; set; }

        [Required, ForeignKey("Purchase")]
        public int Purchase_ID { get; set; }

        public virtual Purchase? Purchase { get; set; }

        [Required, ForeignKey("Order")]
        public int Order_ID { get; set; }

        public virtual Order? Order { get; set; }

        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }
    }
}
