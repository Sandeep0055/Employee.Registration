using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.EntityFramework
{
    public interface IModelProvider
    {
        IModel GetModel();
    }
}
