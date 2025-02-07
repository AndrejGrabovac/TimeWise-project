namespace TimeWise.DTOs.WorkHours;

public class WorkHoursByDateDto
{
    public List<WorkHourDetailDto> WorkHours { get; set; } = new();
    public double TotalHoursWorked { get; set; }
}