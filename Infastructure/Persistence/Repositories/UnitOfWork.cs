using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext storeDb) : IUnitOfWork
    {
        private readonly Dictionary<string,object> _repositories=[];
        public IGenericRepository<TEntity, TKey> GetGenericRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var nameRepo= typeof(TEntity).Name;
            //if (_repositories.ContainsKey(nameRepo))
            //{
            //    return (GenericRepository<TEntity,TKey>) _repositories[nameRepo];
            //}
            if (_repositories.TryGetValue(nameRepo,out object value))
            {
                return (GenericRepository<TEntity, TKey>)value;
            }
            else
            {
                var Repo = new GenericRepository<TEntity, TKey>(storeDb);
                _repositories.Add(nameRepo, Repo);
                return Repo;
            }
        }

        public async Task<int> SaveChangesAsync()=> await storeDb.SaveChangesAsync();
    }
}
