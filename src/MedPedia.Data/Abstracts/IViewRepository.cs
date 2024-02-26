using MedPedia.Common.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;
using Teb.Data.Concretes.Context;

namespace Teb.Data.Abstracts;

public interface IViewRepository<TView> where TView : class, ITableView
{
    AppDbContext DbContext { get; init; }
    DbSet<TView> Entities { get; }
    IQueryable<TView> View { get; }
    IQueryable<TView> ViewNoTracking { get; }
}