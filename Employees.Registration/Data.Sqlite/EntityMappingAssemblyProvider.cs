using System.Reflection;
using Data.Abstraction;

namespace Data.Sqlite
{
    public class EntityMappingAssemblyProvider : IEntityMappingAssemblyProvider
    {
        public Assembly GetEntityMappingAssembly()
        {
            return typeof(EntityMappingAssemblyProvider).Assembly;
        }
    }
}
