using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MWF.Projects.Domain.Entities;
using MWF.Projects.Domain.Interfaces;
using MWF.Projects.Infraestructure.DataContext;

namespace MWF.Projects.Infraestructure.Repositories;

public class MWF.ProjectsRepository : IMWF.ProjectsRepository
{
    private readonly MWF.ProjectsDbContext _context;
    public MWF.ProjectsRepository(MWF.ProjectsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MWF.ProjectsEntity>> FindAll()
    {
        IQueryable<MWF.ProjectsEntity> query = _context.Set<MWF.ProjectsEntity>();

        return await query.AsNoTracking().ToListAsync();
    }
    public IQueryable<MWF.ProjectsEntity> FindByCondition(Expression<Func<MWF.ProjectsEntity, bool>> expression)
    => _context.Set<MWF.ProjectsEntity>().Where(expression).AsNoTracking();

    public async Task Create(MWF.ProjectsEntity entity)
    {
        await _context.Set<MWF.ProjectsEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    public async Task Update(MWF.ProjectsEntity entity)
    {
        _context.Set<MWF.ProjectsEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task Delete(MWF.ProjectsEntity entity)
    {
        _context.Set<MWF.ProjectsEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}