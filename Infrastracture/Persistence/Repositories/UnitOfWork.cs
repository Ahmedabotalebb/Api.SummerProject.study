using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class UnitOfWork (StoreDbcontext _dbContext): IUnitOfWork
    {
        private readonly Dictionary<string, Object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            //Check If the repo is exist 
            var repoName=typeof(TEntity).Name;
            // if exist return it
            if(_repositories.ContainsKey(repoName)) 
                return (IGenericRepository<TEntity,TKey>)_repositories[repoName];
            //if doesn't we will create it 
            else { 
                var Repo = new GenericRepository<TEntity, TKey>(_dbContext);
            // add it to dictionary
                _repositories.Add(repoName, Repo);

                return Repo;
            }
            // return it
        }

        public async Task SaveChangesAsync()=> await _dbContext.SaveChangesAsync();


    }
}
