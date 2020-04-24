using System.Reflection;

namespace Data.Abstraction
{
    public interface IEntityMappingAssemblyProvider
    {
        Assembly GetEntityMappingAssembly();
    }
}
