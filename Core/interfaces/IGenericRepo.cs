using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.interfaces
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync (int id);
        Task<IReadOnlyList<T>> ListAllAsync();

        Task<T> GetEntityWithSpec(ISpec<T> spec);

        Task <IReadOnlyList<T>> ListAsync(ISpec<T> spec);
    }
}