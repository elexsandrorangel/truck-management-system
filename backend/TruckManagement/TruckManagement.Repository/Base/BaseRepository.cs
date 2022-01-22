using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TruckManagement.Models.Entities.Base;
using TruckManagement.Repository.Helpers;
using TruckManagement.Repository.Interfaces.Base;

namespace TruckManagement.Repository.Base
{
    public abstract class BaseRepository<T, C> : IBaseRepository<T>
        where T : BaseEntity
        where C : DbContext
    {
        protected readonly C Context;

        #region Constructor

        protected BaseRepository(C context)
        {
            Context = context;
        }

        #endregion Constructor

        #region Add

        public virtual async Task<T> AddAsync(T t)
        {
            if (t == null)
            {
                throw new ArgumentNullException(nameof(t));
            }
            var data = Context.Set<T>().Add(t);
            await SaveAsync();
            return data.Entity;
        }

        public virtual async Task<IEnumerable<T>> AddAsync(IEnumerable<T> tList)
        {
            Context.Set<T>().AddRange(tList);
            await SaveAsync();
            return tList;
        }

        public virtual async Task<IEnumerable<T>> AddOrUpdateAsync(IEnumerable<T> tList)
        {
            Context.Set<T>().AddOrUpdate(tList);
            await SaveAsync();
            return tList;
        }

        public virtual async Task<T> AddOrUpdateAsync(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            Context.Set<T>().AddOrUpdate(item);
            await SaveAsync();
            return item;
        }

        #endregion Add

        #region Count

        public virtual async Task<long> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().AsNoTracking().LongCountAsync(predicate);
        }

        public virtual async Task<long> CountAsync()
        {
            return await Context.Set<T>().AsNoTracking().LongCountAsync();
        }

        #endregion Count

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id, true);
            if (entity == null)
            {
                throw new InvalidOperationException("Entity does not exists");
            }
            await DeleteAsync(entity);
        }

        public virtual async Task DeleteAsync(T t)
        {
            if (t == null)
            {
                throw new InvalidOperationException("Entity does not exists");
            }
            Context.Entry(t).State = EntityState.Detached;

            Context.Set<T>().Remove(t);
            await SaveAsync();
        }

        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().AsNoTracking().Any(predicate);
        }

        #region Get

        public virtual async Task<IEnumerable<T>> GetAsync(int page = 0, int qty = int.MaxValue, bool track = false)
        {
            if (track)
            {
                return await Context.Set<T>()
                    .OrderBy(a => a.CreatedAt).Skip(page * qty)
                    .Take(qty).ToListAsync();
            }
            return await Context.Set<T>()
                .AsNoTracking()
                .OrderBy(a => a.CreatedAt).Skip(page * qty)
                .Take(qty).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> match, int page = 0, int qty = int.MaxValue, bool track = false)
        {
            if (track)
            {
                return await Context.Set<T>().Where(match)
                    .OrderBy(a => a.CreatedAt)
                    .Skip(page * qty).Take(qty).ToListAsync();
            }
            return await Context.Set<T>().Where(match)
                .AsNoTracking()
                .OrderBy(a => a.CreatedAt)
                .Skip(page * qty).Take(qty).ToListAsync();
        }

        public virtual async Task<T> GetAsync(int id, bool track = false)
        {
            return await GetSingleOrDefaultAsync(x => x.Id == id, track);
        }

        public virtual async Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> match, bool track = false)
        {
            if (track)
            {
                return await Context.Set<T>().FirstOrDefaultAsync(match);
            }
            return await Context.Set<T>().AsNoTracking().FirstOrDefaultAsync(match);
        }

        #endregion Get

        public virtual async Task<int> SaveAsync()
        {

            return await Context.SaveChangesAsync();
        }

        public virtual async Task<T> SaveOrUpdateAsync(T t)
        {
            Context.Set<T>().AddOrUpdate(t);
            await SaveAsync();
            return t;
        }

        public virtual async Task<T> UpdateAsync(T updated)
        {
            return await SaveOrUpdateAsync(updated);
        }
    }
}
