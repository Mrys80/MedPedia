using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using MedPedia.Common.Abstracts;
using MedPedia.Common.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;
using Teb.Data.Abstracts;
using Teb.Data.Concretes.Context;

namespace Teb.Data.Concretes.Repositories;

public class Repository<TEntity> : IRepository<TEntity>, IScopedDependency
    where TEntity : class, IEntity
{
    public AppDbContext DbContext { get; init; }
    public DbSet<TEntity> Entities { get; }
    public virtual IQueryable<TEntity> Table => Entities;
    public virtual IQueryable<TEntity> TableNoTracking => Table.AsNoTracking();

    public Repository(AppDbContext dbContext)
    {
        DbContext = dbContext;
        Entities = DbContext.Set<TEntity>(); // City => Cities
    }

    public async Task<List<T>> RawSqlQuery<T>(string query, Func<DbDataReader, T> map)
    {
        using (var command = DbContext.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            DbContext.Database.OpenConnection();

            using (var result = await command.ExecuteReaderAsync())
            {
                var entities = new List<T>();

                while (result.Read())
                {
                    entities.Add(map(result));
                }

                return entities;
            }
        }
    }

    #region Async Methods

    public virtual async ValueTask<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken)
    {
        return await Entities.FirstOrDefaultAsync(filter, cancellationToken);
    }

    public virtual async ValueTask<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        return await Entities.Where(filter).ToListAsync(cancellationToken);
    }

    public virtual ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
    {
        return Entities.FindAsync(ids, cancellationToken);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
    {
        await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);

        if (saveNow)
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken,
        bool saveNow = true)
    {
        await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);

        if (saveNow)
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
    {
        Entities.Update(entity);

        if (saveNow)
            await DbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken,
        bool saveNow = true)
    {
        Entities.UpdateRange(entities);

        if (saveNow)
            await DbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
    {
        Entities.Remove(entity);

        if (saveNow)
            await DbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken,
        bool saveNow = true)
    {
        Entities.RemoveRange(entities);

        if (saveNow)
            await DbContext.SaveChangesAsync(cancellationToken);
    }

    #endregion

    #region Sync Methods

    public TEntity Get(Expression<Func<TEntity, bool>> filter)
    {
        return Entities.FirstOrDefault(filter);
    }

    public virtual TEntity GetById(params object[] ids)
    {
        return Entities.Find(ids);
    }

    public virtual void Add(TEntity entity, bool saveNow = true)
    {
        Entities.Add(entity);

        if (saveNow)
            DbContext.SaveChanges();
    }


    public virtual void AddRange(IEnumerable<TEntity> entities, bool saveNow = true)
    {
        Entities.AddRange(entities);

        if (saveNow)
            DbContext.SaveChanges();
    }

    public virtual void Update(TEntity entity, bool saveNow = true)
    {
        Entities.Update(entity);

        if (saveNow)
            DbContext.SaveChanges();
    }

    public virtual void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true)
    {
        Entities.UpdateRange(entities);

        if (saveNow)
            DbContext.SaveChanges();
    }

    public virtual void Delete(TEntity entity, bool saveNow = true)
    {
        Entities.Remove(entity);

        if (saveNow)
            DbContext.SaveChanges();
    }

    public virtual void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true)
    {
        Entities.RemoveRange(entities);

        if (saveNow)
            DbContext.SaveChanges();
    }

    #endregion

    #region Attach & Detach

    public virtual void Detach(TEntity entity)
    {
        var entry = DbContext.Entry(entity);
        if (entry != null)
            entry.State = EntityState.Detached;
    }

    public virtual void Attach(TEntity entity)
    {
        if (DbContext.Entry(entity).State == EntityState.Detached)
            Entities.Attach(entity);
    }

    #endregion

    #region Explicit Loading

    // از نوع ICollection
    public virtual async Task LoadCollectionAsync<TProperty>(TEntity entity,
        Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
        where TProperty : class
    {
        Attach(entity);

        var collection = DbContext.Entry(entity).Collection(collectionProperty);
        if (!collection.IsLoaded)
            await collection.LoadAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual void LoadCollection<TProperty>(TEntity entity,
        Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
        where TProperty : class
    {
        Attach(entity);

        var collection = DbContext.Entry(entity).Collection(collectionProperty);
        if (!collection.IsLoaded)
            collection.Load();
    }

    public virtual async Task LoadReferenceAsync<TProperty>(TEntity entity,
        Expression<Func<TEntity, IEnumerable<TProperty>>> referenceProperty, CancellationToken cancellationToken)
        where TProperty : class
    {
        Attach(entity);

        var reference = DbContext.Entry(entity).Reference(referenceProperty);
        if (!reference.IsLoaded)
            await reference.LoadAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual void LoadReference<TProperty>(TEntity entity,
        Expression<Func<TEntity, IEnumerable<TProperty>>> referenceProperty)
        where TProperty : class
    {
        Attach(entity);

        var reference = DbContext.Entry(entity).Reference(referenceProperty);
        if (!reference.IsLoaded)
            reference.Load();
    }

    #endregion
}