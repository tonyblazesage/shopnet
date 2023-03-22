using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private readonly Context _context;
        public GenericRepo(Context context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpec(ISpec<T> spec)
        {
            return await ApplySpec(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpec<T> spec)
        {
            return await ApplySpec(spec).ToListAsync();
        }

        private IQueryable<T> ApplySpec(ISpec<T> spec)
        {
            return SpecEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}