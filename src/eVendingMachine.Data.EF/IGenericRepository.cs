using eVendingMachine.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eVendingMachine.Data.EF
{
    public interface IGenericRepository<TAggregate> : IRepository where TAggregate : Entity, IAggregateRoot
    {
        Task AddAsync(TAggregate entity);
        void Delete(TAggregate entity);
        Task<TAggregate> GetByIdAsync(Guid id);
        void Update(TAggregate entity);
        Task<List<TAggregate>> All();
    }

}
