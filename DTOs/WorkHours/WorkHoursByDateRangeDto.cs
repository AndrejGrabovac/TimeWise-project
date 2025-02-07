namespace TimeWise.DTOs.WorkHours;

public class WorkHoursByDateRangeDto
{
    public List<WorkHourDetailDto> WorkHours { get; set; } = new();
    public double TotalHoursWorked { get; set; }
}