using System.ComponentModel.DataAnnotations;
using FoodTruckApp.Models.WorkForce;
using FoodTruckApp.Models.Menu;

namespace FoodTruckApp.Models.WorkForce
{
    public class Access
    {
        [Key]
        public int Access_Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int PrivilegeLvl { get; set; }
    }
}
