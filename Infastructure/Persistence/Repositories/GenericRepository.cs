﻿using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> (StoreDbContext storeDb): IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        
        public async Task AddAsync(TEntity entity)
        {
          await  storeDb.Set<TEntity>().AddAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
             return  await  storeDb.Set<TEntity>().ToListAsync();
        }

      
        public  async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await storeDb.Set<TEntity>().FindAsync(id);
        }
      

        public void Remove(TEntity entity)
        {
            storeDb.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            storeDb.Set<TEntity>().Update(entity);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        {
         return await  SpecificationEvaluator.CreateQuery(storeDb.Set<TEntity>(), specifications).ToListAsync();
        }


        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
        {
           return await SpecificationEvaluator.CreateQuery(storeDb.Set<TEntity>(),specifications).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
        {
           return await SpecificationEvaluator.CreateQuery(storeDb.Set<TEntity>(), specifications).CountAsync();
        }
    }
}
