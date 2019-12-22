﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Galaxy.Teams.Core.Models;

namespace Galaxy.Teams.Core.Intefaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            string includeProperties = "");
        TEntity GetById(Guid id);
        Task<ActionResponse> AddAsync(TEntity entity);
        Task<ActionResponse> UpdateAsync(TEntity entityToUpdate);
    }
}