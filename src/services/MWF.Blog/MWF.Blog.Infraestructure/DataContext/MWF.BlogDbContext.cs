using Microsoft.EntityFrameworkCore;
using MWF.Blog.Domain.Entities;

namespace MWF.Blog.Infraestructure.DataContext;

public class MWF.BlogDbContext : DbContext
{
    public DbSet<MWF.BlogEntity> MWF.Blog { get; set; }
    public MWF.BlogDbContext(DbContextOptions<MWF.BlogDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
