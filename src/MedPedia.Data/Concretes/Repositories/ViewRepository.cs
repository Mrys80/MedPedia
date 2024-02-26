using MedPedia.Common.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;
using Teb.Data.Abstracts;
using Teb.Data.Concretes.Context;

namespace Teb.Data.Concretes.Repositories;

public class ViewRepository<TView> : IViewRepository<TView> where TView : class, ITableView
{
    public AppDbContext DbContext { get; init; }
    public DbSet<TView> Entities { get; }
    public virtual IQueryable<TView> View => Entities;
    public virtual IQueryable<TView> ViewNoTracking => View.AsNoTracking();

    public ViewRepository(AppDbContext dbContext)
    {
        DbContext = dbContext;
        Entities = DbContext.Set<TView>(); // City => Cities
    }
}