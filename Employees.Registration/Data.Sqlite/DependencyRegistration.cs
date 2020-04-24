using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Data.Abstraction;
using Data.EntityFramework;
using Data.Sqlite;

namespace Data.Sqlite
{
    public static class DependencyRegistration
    {
       
        public static IServiceCollection AddSqliteContextFor<TContext, TContextImpl>(this IServiceCollection services, string connectionString)
          where TContext : IDataContext
          where TContextImpl : DbContext, TContext
        {
            services.TryAddSingleton<IEntityMappingAssemblyProvider, EntityMappingAssemblyProvider>();
            services.TryAddSingleton<IModelProvider, EntityModelProvider>();

            services.AddDbContext<TContext, TContextImpl>((provider, builder) =>
            {
                builder.UseModel(provider.GetRequiredService<IModelProvider>().GetModel())
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
                    .UseLazyLoadingProxies()
                    .UseSqlite(connectionString);
            });

            return services;
        }
    }
}
