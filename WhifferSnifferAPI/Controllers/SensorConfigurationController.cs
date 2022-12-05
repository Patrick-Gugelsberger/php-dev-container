using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhifferSnifferAPI.Data;
using WhifferSnifferAPI.Models;

namespace WhifferSnifferAPI.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class SensorConfigurationController : ControllerBase
    {
        private readonly DataContext _context;

        public SensorConfigurationController(DataContext context) => _context = context;

        [HttpGet("GetConfiguration")]
        [ProducesResponseType(typeof(SensorReading), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetConfiguration()
        {
            var configuration = await _context.SensorConfigurations.ToListAsync();

            return configuration == null ? NotFound() : Ok(configuration);
        }

        [HttpPut("UpdateConfiguration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        public async Task<IActionResult> UpdateSensorConfiguration(SensorConfiguration sensorConfiguration)
        {
            try
            {
                var configuration = await _context.SensorConfigurations.FindAsync(1);

                if (configuration != null)
                {
                    configuration.ReadInterval = sensorConfiguration.ReadInterval;
                    configuration.SyncInterval = sensorConfiguration.SyncInterval;
                    configuration.WarningValue = sensorConfiguration.WarningValue;
                    configuration.CriticalValue = sensorConfiguration.CriticalValue;
                    configuration.Brightness = sensorConfiguration.Brightness;
                    configuration.DiodMode = sensorConfiguration.DiodMode;
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(304);
            }

            return Ok();
        }
    }
}
