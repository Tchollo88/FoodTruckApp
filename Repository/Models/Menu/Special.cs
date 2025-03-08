using System.ComponentModel.DataAnnotations;
using Repository.Models.Client;

namespace Repository.Models.Menu
{
    public class Special
    {
        [Key]
        public int Special_ID { get; set; }

        public string Name { get; set; } = string.Empty;
        public decimal Discount { get; set; } = decimal.MinValue;
        public bool isApplied { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

    }
}
