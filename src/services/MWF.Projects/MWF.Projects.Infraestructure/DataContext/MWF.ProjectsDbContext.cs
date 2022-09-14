using Microsoft.EntityFrameworkCore;
using MWF.Projects.Domain.Entities;

namespace MWF.Projects.Infraestructure.DataContext;

public class MWF.ProjectsDbContext : DbContext
{
    public DbSet<MWF.ProjectsEntity> MWF.Projects { get; set; }
    public MWF.ProjectsDbContext(DbContextOptions<MWF.ProjectsDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
