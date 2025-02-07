using System.Globalization;
using TimeWise.DTOs.WorkHours;
using TimeWise.Models;

namespace TimeWise.Mappers;

public static class WorkHourMapper
{
    public static WorkHourDto ToWorkHourDto(this WorkHour workHour)
    {
        return new WorkHourDto()
        {
            Id = workHour.Id,
            Date = workHour.Date,
            HoursWorked = workHour.HoursWorked
        };
    }
    public static WorkHourDetailDto ToWorkHourDetailDto(this WorkHour workHour)
    {
        return new WorkHourDetailDto()
        {
            Id = workHour.Id,
            Date = workHour.Date,
            HourFrom = workHour.HourFrom,
            HourTo = workHour.HourTo,
            HoursWorked = workHour.HoursWorked
        };
    }
    
    public static WorkHour ToWorkHour(this WorkHourCreateDto workHourCreateDto)
    {
        return new WorkHour()
        {
            Date = workHourCreateDto.Date,
            HourFrom = TimeOnly.ParseExact(workHourCreateDto.HourFrom!, "HH:mm", CultureInfo.InvariantCulture),
            HourTo = TimeOnly.ParseExact(workHourCreateDto.HourTo!, "HH:mm", CultureInfo.InvariantCulture)
        };
    }
    
    public static WorkHour ToWorkHour(this WorkHourUpdateDto workHourUpdateDto)
    {
        return new WorkHour()
        {
            Id = workHourUpdateDto.Id,
            Date = workHourUpdateDto.Date,
            HourFrom = TimeOnly.ParseExact(workHourUpdateDto.HourFrom!, "HH:mm", CultureInfo.InvariantCulture),
            HourTo = TimeOnly.ParseExact(workHourUpdateDto.HourTo!, "HH:mm", CultureInfo.InvariantCulture)
        };
    }
}