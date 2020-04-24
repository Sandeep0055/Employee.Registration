using Data.Abstraction;
using Data.Abstraction.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.EntityFramework
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public async Task InsertAsync<TEntity>(TEntity entity, bool shouldFlush = false,
         CancellationToken cancellationToken = default)
         where TEntity : Abstraction.Models.IModel
        {
            await AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (shouldFlush)
            {
                await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task UpdateAsync<TEntity>(TEntity entity, bool shouldFlush = false,
            CancellationToken cancellationToken = default)
            where TEntity : Abstraction.Models.IModel
        {
            Update(entity);
            if (shouldFlush)
            {
                await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task InsertOrUpdateAsync<TEntity>(TEntity entity, bool shouldFlush = false,
            CancellationToken cancellationToken = default)
            where TEntity : class, Abstraction.Models.IModel
        {
            var result = await FindAsync(entity.GetType(), entity.Id)
                .ConfigureAwait(false);

            if (result != null)
            {
                Entry(result).State = EntityState.Detached;
                Remove(result);
                await UpdateAsync(entity, shouldFlush, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                await InsertAsync(entity, shouldFlush, cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync<TEntity>(TEntity entity, bool shouldFlush = false,
            CancellationToken cancellationToken = default)
            where TEntity : Abstraction.Models.IModel
        {

            Entry(entity).State = EntityState.Deleted;
            if (shouldFlush)
            {
                await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        public IQueryable<TEntity> QueryBuilder<TEntity>() where TEntity : class, Abstraction.Models.IModel
        {
            return Set<TEntity>();
        }

        public IQueryable<TEntity> NoTrackingQueryBuilder<TEntity>() where TEntity : class, IModel
        {
            return Set<TEntity>().AsNoTracking();
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task SaveTransactionAsync<TEntity>(IEnumerable<IOperation<TEntity>> operations,
            CancellationToken cancellationToken = default) where TEntity : Abstraction.Models.IModel
        {
            foreach (var operation in operations)
            {
                switch (operation.OperationType)
                {
                    case OperationTypes.Insert:
                        await AddAsync(operation.Entity, cancellationToken).ConfigureAwait(false);
                        break;
                    case OperationTypes.Update:
                        Update(operation.Entity);
                        break;
                    case OperationTypes.Delete:
                        Entry(operation.Entity).State = EntityState.Deleted;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<bool> CreateBackendAsync(CancellationToken cancellationToken = default)
        {
            var result = await Database.EnsureCreatedAsync(cancellationToken).ConfigureAwait(false);
            return result;
        }

        public async Task<bool> ExecuteCommands(string fileName, string separator, CancellationToken cancellationToken = default)
        {
            var fileStream = new FileStream(fileName, FileMode.Open);
            using var reader = new StreamReader(fileStream);
            var line = await reader.ReadToEndAsync().ConfigureAwait(false);
            var query = line.Split(separator);

            await using (var connection = Database.GetDbConnection())
            {
                await using var cmd = connection.CreateCommand();
                connection.Open();
                foreach (var insertQuery in query)
                {
                    if (string.IsNullOrWhiteSpace(insertQuery)) return true;
                    {
                        cmd.CommandText = insertQuery;
                        await cmd.ExecuteNonQueryAsync(cancellationToken);
                    }
                }
            }

            return true;
        }


    }
}
