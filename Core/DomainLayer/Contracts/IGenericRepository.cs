﻿using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
  public  interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(); 
        Task<TEntity?> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity,TKey> specifications);
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove (TEntity entity);
        Task<int> CountAsync(ISpecifications<TEntity,TKey>specifications);
    }
}
