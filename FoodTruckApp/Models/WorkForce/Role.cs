using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodTruckApp.Models.WorkForce;
using FoodTruckApp.Models.Menu;

namespace FoodTruckApp.Models.WorkForce
{
    public class Role
    {
        [Key]
        public int Role_ID { get; set; }

        [Required, ForeignKey("User")]
        public int User_ID { get; set; }

        public virtual User? User { get; set; }


        public virtual ICollection<Access>? Access { get; set; } = new List<Access>();
    }
}
