﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Disco.Domain.Interfaces
{
    public interface IRepository<T,TKey>
    {
        Task AddAsync(T item);
        Task Remove(TKey id);
        Task<T> GetAsync(TKey id);
        Task<List<T>> GetAll(Expression<Func<T,bool>> expression);
        Task<T> Update(T newItem);
    }
}
