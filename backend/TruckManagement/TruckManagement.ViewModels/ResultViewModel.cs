using System.Text.Json;
using System.Text.Json.Serialization;

namespace TruckManagement.ViewModels
{
    public class ResultViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public string ExceptionType { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, options: new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
        }
    }
}
