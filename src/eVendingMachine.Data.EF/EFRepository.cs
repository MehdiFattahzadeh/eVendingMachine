using eVendingMachine.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eVendingMachine.Data.EF
{
    public class EFRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        protected readonly DbContext _db;
        public EFRepository(DbContext dbContext) => _db = dbContext;
        public Task AddAsync(TEntity entity) => _db.Set<TEntity>().AddAsync(entity).AsTask();
        public void Delete(TEntity entity) => _db.Set<TEntity>().Remove(entity);
        public void DeleteRange(ICollection<TEntity> entities) => _db.Set<TEntity>().RemoveRange(entities);
        public void Update(TEntity entity) => _db.Update(entity);
        public virtual Task<List<TEntity>> All() => _db.Set<TEntity>().ToListAsync();
        public virtual Task<TEntity> GetByIdAsync(Guid id) => _db.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
    }
}
