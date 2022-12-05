using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhifferSnifferAPI.Data;

namespace WhifferSnifferAPI.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class SensorReadingController : ControllerBase
    {
        private readonly DataContext _context;

        public SensorReadingController(DataContext context) => _context = context;

        [HttpGet("All")]
        public async Task<IEnumerable<SensorReading>> GetSensorReadings()
        {
            return await _context.SensorReadings.ToListAsync();
        }

        [HttpGet("Guid")]
        [ProducesResponseType(typeof(SensorReading), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSensorReadingById(Guid guid)
        {
            var reading = await _context.SensorReadings.FindAsync(guid);
            return reading == null ? NotFound() : Ok(reading);
        }

        [HttpGet("SingleCo2")]
        [ProducesResponseType(typeof(IEnumerable<SensorReading>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<SensorReading>> GetSensorReadingsByCO2(double co2)
        {
            var readings = await _context.SensorReadings.ToListAsync();

            var sortedReadings = new List<SensorReading>();

            foreach (var reading in readings)
                if (reading.Co2Value == co2)
                    sortedReadings.Add(reading);

            return sortedReadings;
        }

        [HttpGet("Co2Span")]
        [ProducesResponseType(typeof(IEnumerable<SensorReading>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<SensorReading>> GetSensorReadingsByCo2Span(double startCo2, double endCo2)
        {
            var readings = await _context.SensorReadings.ToListAsync();

            var sortedReadings = new List<SensorReading>();

            foreach (var reading in readings)
                if (reading.Co2Value >= startCo2 && reading.Co2Value <= endCo2)
                    sortedReadings.Add(reading);

            return sortedReadings;
        }

        [HttpGet("SingleTemperature")]
        [ProducesResponseType(typeof(IEnumerable<SensorReading>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<SensorReading>> GetSensorReadingsByTemperature(double temperature)
        {
            var readings = await _context.SensorReadings.ToListAsync();

            var sortedReadings = new List<SensorReading>();

            foreach (var reading in readings)
                if (reading.TemperatureValue == temperature)
                    sortedReadings.Add(reading);

            return sortedReadings;
        }

        [HttpGet("TemperatureSpan")]
        [ProducesResponseType(typeof(IEnumerable<SensorReading>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<SensorReading>> GetSensorReadingsByTemperatureSpan(double startTemperature, double endTemperature)
        {
            var readings = await _context.SensorReadings.ToListAsync();

            var sortedReadings = new List<SensorReading>();

            foreach (var reading in readings)
                if (reading.TemperatureValue >= startTemperature && reading.TemperatureValue <= endTemperature)
                    sortedReadings.Add(reading);

            return sortedReadings;
        }

        [HttpGet("SingleHumidity")]
        [ProducesResponseType(typeof(IEnumerable<SensorReading>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<SensorReading>> GetSensorReadingsByHumidity(double humidity)
        {
            var readings = await _context.SensorReadings.ToListAsync();

            var sortedReadings = new List<SensorReading>();

            foreach (var reading in readings)
                if (reading.HumidityValue == humidity)
                    sortedReadings.Add(reading);

            return sortedReadings;
        }

        [HttpGet("HumiditySpan")]
        [ProducesResponseType(typeof(IEnumerable<SensorReading>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<SensorReading>> GetSensorReadingsByHumiditySpan(double startHumidity, double endHumidity)
        {
            var readings = await _context.SensorReadings.ToListAsync();

            var sortedReadings = new List<SensorReading>();

            foreach (var reading in readings)
                if (reading.HumidityValue >= startHumidity && reading.HumidityValue <= endHumidity)
                    sortedReadings.Add(reading);

            return sortedReadings;
        }

        [HttpGet("SingleDate")]
        [ProducesResponseType(typeof(IEnumerable<SensorReading>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<SensorReading>> GetSensorReadingsByDate(DateTime date)
        {
            var readings = await _context.SensorReadings.ToListAsync();

            var sortedReadings = new List<SensorReading>();
            
            foreach (var reading in readings)            
                if (reading.Time!.Value.Date == date.Date)               
                    sortedReadings.Add(reading);
                           
            return sortedReadings;
        }

        [HttpGet("DateSpan")]
        [ProducesResponseType(typeof(IEnumerable<SensorReading>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<SensorReading>> GetSensorReadingsByDateSpan(DateTime startDate, DateTime endDate)
        {
            var readings = await _context.SensorReadings.ToListAsync();

            var sortedReadings = new List<SensorReading>();

            foreach (var reading in readings)
                if (reading.Time!.Value.Date >= startDate.Date && reading.Time!.Value.Date <= endDate.Date)
                    sortedReadings.Add(reading);

            return sortedReadings;
        }

        [HttpGet("DateTimeSpan")]
        [ProducesResponseType(typeof(IEnumerable<SensorReading>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<SensorReading>> GetSensorReadingsByDateTimeSpan(DateTime startDate, DateTime endDate)
        {
            var readings = await _context.SensorReadings.ToListAsync();

            var sortedReadings = new List<SensorReading>();

            foreach (var reading in readings)
                if (reading.Time!.Value >= startDate && reading.Time!.Value <= endDate)
                    sortedReadings.Add(reading);

            return sortedReadings;
        }

        [HttpPost("CreateSingleSensorReading")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> NewSensorReading(SensorReading sensorReading)
        {
            sensorReading.Id = Guid.NewGuid();
            sensorReading.Time = DateTime.Now;

            await _context.SensorReadings.AddAsync(sensorReading);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSensorReadingById), new {id = sensorReading.Id}, sensorReading);
        }

        [HttpPost("CreateMultipleSensorReadings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> NewSensorReadingList(List<SensorReading> SensorReadings)
        {
            foreach (var sensorReading in SensorReadings)
            {
                sensorReading.Id = Guid.NewGuid();
                sensorReading.Time = DateTime.Now;

                await _context.SensorReadings.AddAsync(sensorReading);
            }
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
