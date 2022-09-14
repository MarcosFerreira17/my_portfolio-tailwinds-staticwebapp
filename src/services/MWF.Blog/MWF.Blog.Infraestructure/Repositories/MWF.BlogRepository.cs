using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MWF.Blog.Domain.Entities;
using MWF.Blog.Domain.Interfaces;
using MWF.Blog.Infraestructure.DataContext;

namespace MWF.Blog.Infraestructure.Repositories;

public class MWF.BlogRepository : IMWF.BlogRepository
{
    private readonly MWF.BlogDbContext _context;
    public MWF.BlogRepository(MWF.BlogDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MWF.BlogEntity>> FindAll()
    {
        IQueryable<MWF.BlogEntity> query = _context.Set<MWF.BlogEntity>();

        return await query.AsNoTracking().ToListAsync();
    }
    public IQueryable<MWF.BlogEntity> FindByCondition(Expression<Func<MWF.BlogEntity, bool>> expression)
    => _context.Set<MWF.BlogEntity>().Where(expression).AsNoTracking();

    public async Task Create(MWF.BlogEntity entity)
    {
        await _context.Set<MWF.BlogEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    public async Task Update(MWF.BlogEntity entity)
    {
        _context.Set<MWF.BlogEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task Delete(MWF.BlogEntity entity)
    {
        _context.Set<MWF.BlogEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}