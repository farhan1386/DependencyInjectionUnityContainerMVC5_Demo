using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DependencyInjectionUnityContainerMVC5_Demo.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int? id);
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
