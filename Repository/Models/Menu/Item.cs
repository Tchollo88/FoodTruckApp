﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models.Menu
{
    public class Item
    {
        [Key]
        public int Item_ID { get; set; }

        public string Image { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;
        [Required]
        public string Category { get; set; } = string.Empty;

        public bool OnSale { get; set; } = false;

        [Range(0, 100)]
        public int DiscountPercentage { get; set; } = 10;

    }
}
