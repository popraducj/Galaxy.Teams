using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Galaxy.Teams.Core.Intefaces;
using Galaxy.Teams.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Galaxy.Teams.Infrastructure
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly TeamDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(TeamDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties == null) return orderBy != null ? orderBy(query).ToList() : query.ToList();

            foreach (var includeProperty in includeProperties.Split
                (new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }


            return orderBy != null ? await (query).ToListAsync() : await query.ToListAsync();
        }

        public virtual TEntity GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task UpdateAsync(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}