using Data;
using Data.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks; 

namespace Data.Abstraction
{
    public interface IDataContext : IAsyncDisposable, IDisposable
    {
        Task InsertAsync<TEntity>(TEntity entity, bool shouldFlush = false, CancellationToken cancellationToken = default) where TEntity : IModel;

        Task UpdateAsync<TEntity>(TEntity entity, bool shouldFlush = false, CancellationToken cancellationToken = default) where TEntity : IModel;

        Task InsertOrUpdateAsync<TEntity>(TEntity entity, bool shouldFlush = false, CancellationToken cancellationToken = default)
            where TEntity : class, IModel;

        Task DeleteAsync<TEntity>(TEntity entity, bool shouldFlush = false, CancellationToken cancellationToken = default) where TEntity : IModel;

        IQueryable<TEntity> QueryBuilder<TEntity>() where TEntity : class, IModel;

        IQueryable<TEntity> NoTrackingQueryBuilder<TEntity>() where TEntity : class, IModel;

        Task SaveAsync(CancellationToken cancellationToken = default);

        Task SaveTransactionAsync<TEntity>(IEnumerable<IOperation<TEntity>> operations = null, CancellationToken cancellationToken = default) where TEntity : IModel;

        Task<bool> CreateBackendAsync(CancellationToken cancellationToken = default);

        Task<bool> ExecuteCommands(string fileName, string separator, CancellationToken cancellationToken = default);

    }
}
