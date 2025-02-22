using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodTruckApp.Models
{
    public class Role
    {
        [Key]
        public int Role_ID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Privilege { get; set; } = string.Empty;

        public virtual ICollection<User>? Users { get; set; } = new List<User>();
    }
}
