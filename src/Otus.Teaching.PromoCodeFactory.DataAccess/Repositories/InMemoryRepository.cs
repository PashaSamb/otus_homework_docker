using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly List<T> Data;

        public InMemoryRepository(IEnumerable<T> data)
        {
            Data = data.ToList();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(Data.AsEnumerable());
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            var entity = Data.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(entity);
        }

        public Task<T> AddAsync(T entity)
        {
            entity.Id = Guid.NewGuid();
            Data.Add(entity);
            return Task.FromResult(entity);
        }

        public Task UpdateAsync(T entity)
        {
            var index = Data.FindIndex(x => x.Id == entity.Id);
            if (index != -1)
            {
                Data[index] = entity;
            }
            else
            {
                throw new KeyNotFoundException("Entity with the given id not found.");
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            var existingEntity = Data.FirstOrDefault(x => x.Id == entity.Id);
            if (existingEntity != null)
            {
                Data.Remove(existingEntity);
            }
            else
            {
                throw new KeyNotFoundException("Entity with the given id not found.");
            }
            return Task.CompletedTask;
        }

        public Task<IEnumerable<T>> GetByExpressionAsync(Expression<Func<T, bool>> predicate)
        {
            var query = Data.AsQueryable().Where(predicate);
            return Task.FromResult(query.AsEnumerable());
        }
    }
}