using E_Vision.Core.Interfaces.Repository.Base;
using E_Vision.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static E_Vision.SharedKernel.Enum.SharedKernelEnums;

namespace E_Vision.Infrastructure.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Defines the context
        /// </summary>
        private readonly AppDbContext context;

        /// <summary>
        /// Defines the entities
        /// </summary>
        private DbSet<T> entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="ElectricityCorrespondenceContext"/></param> 
        public BaseRepository(AppDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        #region Insert
        public T Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            return entities.Add(entity).Entity;
        }
        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            return (await entities.AddAsync(entity)).Entity;
        }
        public void BulkInsert(List<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");
            // entities.AddRange(entities);
            context.AddRangeAsync(entities);
        }
        #endregion

        #region Update
        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            entities.Update(entity);
        }
        #endregion

        #region Delete
        public void BulkHardDelete(Expression<Func<T, bool>> filter = null)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");
            entities.RemoveRange(entities.Where(filter));
        }
        #endregion

        #region Retreive

        #region GetById
        public async Task<T> GetByIdIfNotDeleted(int Id)
        {
            T record = await entities.FindAsync(Id);
            //context.Entry(record).State = EntityState.Detached;
            if (record != null)
            {
                var property = record.GetType().GetProperties().FirstOrDefault(a => a.Name == "IsDeleted" /*According to system naming convension here*/);
                if (property != null && (bool)property.GetValue(record))
                    return null;
                else
                    context.Entry(record).State = EntityState.Detached;
            }
            return record;
        }
        #endregion

        #region GetList
        public async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = entities;
            if (filter != null)
                query = query.Where(filter);
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.AsNoTracking().ToListAsync();
        }
        #endregion

        #region Get Paged
        public async Task<List<T>> GetPageAsync<TKey>(int PageNumeber, int PageSize, Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> sortingExpression, SortDirection sortDir = SortDirection.Ascending, string includeProperties = "")
        {
            IQueryable<T> query = context.Set<T>();
            int skipCount = (PageNumeber - 1) * PageSize;

            if (filter != null)
            {
                query = query.Where(filter);

            }

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            switch (sortDir)
            {
                case SortDirection.Ascending:
                    if (skipCount == 0)
                        query = query.OrderBy<T, TKey>(sortingExpression).Take(PageSize);
                    else
                        query = query.OrderBy<T, TKey>(sortingExpression).Skip(skipCount).Take(PageSize);
                    break;
                case SortDirection.Descending:
                    if (skipCount == 0)
                        query = query.OrderByDescending<T, TKey>(sortingExpression).Take(PageSize);
                    else
                        query = query.OrderByDescending<T, TKey>(sortingExpression).Skip(skipCount).Take(PageSize);
                    break;
                default:
                    break;
            }
            return await query.AsNoTracking().ToListAsync();
        }
        #endregion

        #region Get Individuals
        public async Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null)
        {
            return await entities.CountAsync(filter);
        }
        public async Task<bool> GetAnyAsync(Expression<Func<T, bool>> filter = null)
        {
            return await entities.AnyAsync(filter);
        }
        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = context.Set<T>();
            if (filter != null)
                query = query.Where(filter).AsNoTracking();
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            T record = await query.FirstOrDefaultAsync();
            if (record != default)
                 context.Entry(record).State = EntityState.Detached;
            return record;
        }
        public async Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = context.Set<T>();
            if (filter != null)
                query = query.Where(filter).AsNoTracking();
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            T record = await query.OrderByDescending(item => item).FirstOrDefaultAsync();
            if (record != default)
                context.Entry(record).State = EntityState.Detached;
            return record;
        }

        public async Task<int> GetMaxAsync()
        {
            return default;
        }
        #endregion



        #endregion
    }
}
