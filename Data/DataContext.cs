using Microsoft.EntityFrameworkCore;
using TimeWise.Models;

namespace TimeWise.Data;

public class DataContext : DbContext
{
    public DbSet<WorkHour> WorkHours { get; set; }
    
    public DataContext(DbContextOptions options) : base(options)
    {
        
    }
}