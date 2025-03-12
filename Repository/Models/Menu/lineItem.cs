using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Menu
{
    public class lineItem
    {
        [Key]
        public int lineItem_ID { get; set; }

        [ForeignKey("Item")]
        public int Item_ID { get; set; }
        public virtual Item? Item { get; set; }

        public int Quantity { get; set; }
      
        [ForeignKey("Order")]
        public int Order_ID { get; set; }
        public virtual Order? Order { get; set; }
    }
}
