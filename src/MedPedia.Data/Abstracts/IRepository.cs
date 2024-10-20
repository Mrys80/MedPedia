using System.Data.Common;
using System.Linq.Expressions;
using MedPedia.Common.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;
using Teb.Data.Concretes.Context;

namespace Teb.Data.Abstracts;
public interface IRepository<TEntity> where TEntity : class, IEntity
{
    AppDbContext DbContext { get; init; }
    DbSet<TEntity> Entities { get; }
    IQueryable<TEntity> Table { get; }
    IQueryable<TEntity> TableNoTracking { get; }

    Task<List<T>> RawSqlQuery<T>(string query, Func<DbDataReader, T> map);
    void Add(TEntity entity, bool saveNow = true);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
    void AddRange(IEnumerable<TEntity> entities, bool saveNow = true);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
    void Attach(TEntity entity);
    void Delete(TEntity entity, bool saveNow = true);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
    void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
    void Detach(TEntity entity);
    TEntity GetById(params object[] ids);
    TEntity Get(Expression<Func<TEntity, bool>> filter);
    ValueTask<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    
    ValueTask<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);

    ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
    void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty) where TProperty : class;
    Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken) where TProperty : class;
    void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> referenceProperty) where TProperty : class;
    Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> referenceProperty, CancellationToken cancellationToken) where TProperty : class;
    void Update(TEntity entity, bool saveNow = true);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
    void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);

}