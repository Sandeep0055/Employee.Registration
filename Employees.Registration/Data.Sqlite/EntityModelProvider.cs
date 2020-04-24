using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Data.Abstraction;
using Data.EntityFramework;

namespace Data.Sqlite
{
    public class EntityModelProvider : IModelProvider
    {
        private readonly IEntityMappingAssemblyProvider _assemblyProvider;

        public EntityModelProvider(IEntityMappingAssemblyProvider assemblyProvider)
        {
            _assemblyProvider = assemblyProvider;
        }



        public IModel GetModel()
        {
            var conventionSet = SqliteConventionSetBuilder.Build();
            var modelBuilder = new ModelBuilder(conventionSet);

            modelBuilder.ApplyConfigurationsFromAssembly(_assemblyProvider.GetEntityMappingAssembly());

            return modelBuilder.Model.FinalizeModel();
        }
    }
}
