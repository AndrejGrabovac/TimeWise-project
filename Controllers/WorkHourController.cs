using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeWise.Data;
using TimeWise.DTOs.WorkHours;
using TimeWise.Mappers;
using TimeWise.Models;

namespace TimeWise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class WorkHourController : ControllerBase
    {
        private readonly DataContext _context;

        public WorkHourController(DataContext context)
        {
            _context = context;
        }

        // GET: api/WorkHour
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkHourDto>>> GetWorkHours()
        {
            var workHours = await _context.WorkHours.ToListAsync();

            return Ok(workHours.Select(workHour => workHour.ToWorkHourDto()).ToList());
        }

        // GET: api/WorkHour/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkHourDetailDto>> GetWorkHour(int id)
        {
            var workHour = await _context.WorkHours
                .OrderBy(wh => wh.Date)
                .FirstOrDefaultAsync(wh => wh.Id == id);

            if (workHour == null)
            {
                return NotFound();
            }

            return Ok(workHour.ToWorkHourDetailDto());
        }
        
        // GET: api/WorkHour/ByDate?date=2023-10-01
        [HttpGet("ByDate")]
        public async Task<ActionResult<WorkHoursByDateDto>> GetWorkHoursByDate([FromQuery] DateOnly date)
        {
            var workHours = await _context.WorkHours
                .Where(wh => wh.Date == date)
                .ToListAsync();

            if (workHours.Count == 0)
            {
                return NotFound("No work hours found for the specified date.");
            }

            double totalHours = workHours.Sum(wh => wh.HoursWorked.TotalHours);

            var result = new WorkHoursByDateDto
            {
                WorkHours = workHours.Select(wh => wh.ToWorkHourDetailDto()).ToList(),
                TotalHoursWorked = totalHours
            };

            return Ok(result);
        }
        
        // GET: api/WorkHour/ByDateRange?startDate=2023-10-01&endDate=2023-10-07
        [HttpGet("ByDateRange")]
        public async Task<ActionResult<WorkHoursByDateRangeDto>> GetWorkHoursByDateRange([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
        {
            var workHours = await _context.WorkHours
                .Where(wh => wh.Date >= startDate && wh.Date <= endDate)
                .ToListAsync();

            if (workHours.Count == 0)
            {
                return NotFound("No work hours found for the specified date range.");
            }

            double totalHours = workHours.Sum(wh => wh.HoursWorked.TotalHours);

            var result = new WorkHoursByDateRangeDto
            {
                WorkHours = workHours.Select(wh => wh.ToWorkHourDetailDto()).ToList(),
                TotalHoursWorked = totalHours
            };

            return Ok(result);
        }

        // PUT: api/WorkHour/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkHour(int id, WorkHourUpdateDto workHourUpdateDto)
        {
            if (id != workHourUpdateDto.Id)
            {
                return BadRequest();
            }

            if (!TimeOnly.TryParseExact(workHourUpdateDto.HourFrom, "HH:mm", null, System.Globalization.DateTimeStyles.None, out TimeOnly parsedHourFrom) ||
                !TimeOnly.TryParseExact(workHourUpdateDto.HourTo, "HH:mm", null, System.Globalization.DateTimeStyles.None, out TimeOnly parsedHourTo))
            {
                return BadRequest("Invalid time format. Please use HH:mm. Hours should be between 00 and 23, and minutes should be between 00 and 59.");
            }
            /*
            if (!workHourUpdateDto.IsValidTimeFormat())
            {
                return BadRequest("Invalid time format. Hours should be between 00 and 23, and minutes should be between 00 and 59.");
            }
            */

            WorkHour? workHour = await _context.WorkHours.FindAsync(id);
            
            if (workHour == null)
            {
                return NotFound();
            }
            
            workHour.Date = workHourUpdateDto.Date;
            workHour.HourFrom = parsedHourFrom;
            workHour.HourTo = parsedHourTo;
            workHour.CalculateHoursWorked();
            
            _context.Entry(workHour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!WorkHourExists(id))
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        // POST: api/WorkHour
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkHour>> PostWorkHour([FromBody]WorkHourCreateDto workHourCreateDto)
        {
            if (!TimeOnly.TryParseExact(workHourCreateDto.HourFrom, "HH:mm", null, System.Globalization.DateTimeStyles.None, out _) ||
                !TimeOnly.TryParseExact(workHourCreateDto.HourTo, "HH:mm", null, System.Globalization.DateTimeStyles.None, out _))
            {
                return BadRequest("Invalid time format. Please use HH:mm.Hours should be between 00 and 23, and minutes should be between 00 and 59");
            }
            /*
            if (!workHourCreateDto.IsValidTimeFormat())
            {
                return BadRequest("Invalid time format. Hours should be between 00 and 23, and minutes should be between 00 and 59.");
            }
            */
            
            WorkHour workHour = workHourCreateDto.ToWorkHour();
            workHour.CalculateHoursWorked();
            _context.WorkHours.Add(workHour);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetWorkHour", new { id = workHour.Id }, workHour);
        }
        


        // DELETE: api/WorkHour/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkHour(int id)
        {
            var workHour = await _context.WorkHours.FindAsync(id);
            if (workHour == null)
            {
                return NotFound();
            }

            _context.WorkHours.Remove(workHour);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkHourExists(int id)
        {
            return _context.WorkHours.Any(e => e.Id == id);
        }

    }
}
