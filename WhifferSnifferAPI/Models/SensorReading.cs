using System.ComponentModel.DataAnnotations;

namespace WhifferSnifferAPI
{
    public class SensorReading
    {
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public DateTime? Time { get; set; }
        public double Co2Value { get; set; }
        public double TemperatureValue { get; set; }
        public double HumidityValue { get; set; }
    }
}
