namespace TimeWise.DTOs.WorkHours;

public class WorkHourDto
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeSpan HoursWorked { get; set; }
}