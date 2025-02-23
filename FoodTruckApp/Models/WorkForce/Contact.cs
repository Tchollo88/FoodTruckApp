using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodTruckApp.Models.WorkForce;
using FoodTruckApp.Models.Menu;

namespace FoodTruckApp.Models.WorkForce
{
    public class Contact
    {
        [Key]
        public int Contact_Id { get; set; }

        [Required, ForeignKey("User")]
        public int User_ID { get; set; }

        public virtual User? User { get; set; }

        public virtual ICollection<Phone>? PhoneNumbers { get; set; } = new List<Phone>();
    }
}
