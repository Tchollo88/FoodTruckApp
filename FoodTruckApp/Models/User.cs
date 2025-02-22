using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruckApp.Models
{
    public class User
    {
        [Key]
        public int User_ID { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required, ForeignKey("Account")]
        public int Account_ID { get; set; }

        public virtual Account? Account { get; set; }
    }
}
