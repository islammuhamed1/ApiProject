﻿using Domain.Entities;
namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity ,TKey> where TEntity:BaseEntity<TKey>
    {
        Task<TEntity?> GetAsync(TKey id);
        Task<TEntity?> GetAsync(Specification<TEntity> specifications);
        Task<IEnumerable<TEntity>> GetAllAsync(bool isTrackable = false);
        Task<IEnumerable<TEntity>> GetAllAsync(Specification<TEntity> specifications);
        Task<int> CountAsync(Specification<TEntity> specification);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
