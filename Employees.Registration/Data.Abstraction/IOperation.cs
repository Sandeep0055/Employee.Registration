using Data.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Abstraction
{
    public interface IOperation<TEntity> where TEntity : Models.IModel
    {
        OperationTypes OperationType { get; set; }

        TEntity Entity { get; set; }
    }
}
