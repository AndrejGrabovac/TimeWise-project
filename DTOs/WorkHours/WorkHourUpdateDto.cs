﻿using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TimeWise.DTOs.WorkHours;

public partial class WorkHourUpdateDto
{   
    public int Id { get; set; }

    [Required]
    public DateOnly Date { get; set; }
    
    [Required]
    public string? HourFrom { get; set; }
    
    [Required]
    public string? HourTo { get; set; }
    
    public bool IsValidTimeFormat()
    {
        return TimeRegex().IsMatch(HourFrom!) &&
               TimeRegex().IsMatch(HourTo!);
    }

    [GeneratedRegex(@"^(?:[01]\d|2[0-3]):[0-5]\d$")]
    private static partial Regex TimeRegex();
}