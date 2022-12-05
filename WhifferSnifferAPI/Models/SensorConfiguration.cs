using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WhifferSnifferAPI.Models
{
    public class SensorConfiguration
    {
        public int Id { get; set; } = 1;
        public int ReadInterval { get; set; }
        public int SyncInterval { get; set; }
        public int WarningValue { get; set; }
        public int CriticalValue { get; set; }
        public int Brightness { get; set; }
        public DiodModes DiodMode { get; set; }
    }
}
