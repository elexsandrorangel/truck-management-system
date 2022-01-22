using System.Text.Json.Serialization;
using TruckManagement.Models.Entities.Enums;
using TruckManagement.ViewModels.Base;

namespace TruckManagement.ViewModels
{
    public class TruckViewModel : BaseViewModel
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TruckModelEnum Model { get; set; }

        public int ManufactureYear { get; set; }

        public int ModelYear { get; set; }

        public string Color { get; set; }
    }
}
