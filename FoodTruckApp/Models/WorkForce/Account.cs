using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodTruckApp.Models.WorkForce;
using FoodTruckApp.Models.Menu;

namespace FoodTruckApp.Models.WorkForce
{
    public class Account
    {
        [Key]
        public int Account_ID { get; set; }

        [Required, ForeignKey("User")]
        public int User_ID { get; set; }

        public virtual User? User { get; set; }

        [Required, ForeignKey("Info")]
        public int Info_ID { get; set; }

        public virtual Info? Info { get; set; }
    }
}
