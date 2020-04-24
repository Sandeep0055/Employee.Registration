using Data.Abstraction;
using Data.Abstraction.Models; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Abstraction
{
    public class Operation<TEntity> : IOperation<TEntity> where TEntity : IModel
    {
        public OperationTypes OperationType { get; set; }
        public TEntity Entity { get; set; }
    }
}
