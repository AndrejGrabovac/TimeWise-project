
namespace TimeWise.Models;

public class WorkHour
{   
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly HourFrom { get; set; }
    public TimeOnly HourTo { get; set; }
    public TimeSpan HoursWorked { get; set; }
    
    
    public void CalculateHoursWorked()
    {
        HoursWorked = HourTo - HourFrom;
    }
}

