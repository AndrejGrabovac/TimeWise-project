namespace TimeWise.DTOs.WorkHours;

public class WorkHourDetailDto
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly HourFrom { get; set; }
    public TimeOnly HourTo { get; set; }
    public TimeSpan HoursWorked { get; set; }
}