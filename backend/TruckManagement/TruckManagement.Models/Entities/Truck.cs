using System.ComponentModel.DataAnnotations;
using TruckManagement.Models.Entities.Base;
using TruckManagement.Models.Entities.Enums;

namespace TruckManagement.Models.Entities
{
    public class Truck : BaseEntity
    {
        [Required]
        public TruckModelEnum Model { get; set; }

        [Required]
        public int ManufactureYear { get; set; }

        [Required]
        public int ModelYear { get; set; }

        public string Color { get; set; }
    }
}
