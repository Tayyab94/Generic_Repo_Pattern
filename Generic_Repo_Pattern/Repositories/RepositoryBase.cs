using Generic_Repo_Pattern.Interfaces;
using Generic_Repo_Pattern.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Generic_Repo_Pattern.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;

        public RepositoryBase(AppDbContext context)
        {
            this._context = context;
        }

        public void Add(TEntity objModel)
        {
            _context.Set<TEntity>().Add(objModel);
            _context.SaveChanges();
        }

        public void AddRange(IEnumerable<TEntity> objModel)
        {
            _context.Set<TEntity>().AddRange(objModel);
            _context.SaveChanges(); 
        }

        public int Count()
        {
            return  _context.Set<TEntity>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<TEntity>().CountAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

      

        public TEntity? Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);   
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public TEntity? GetId(int id)
        {
            return _context.Set<TEntity>().Find(id);

        }

        public async Task<TEntity?> GetIdAsync(int id)
             => await _context.Set<TEntity>().FindAsync(id);


        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return
                await Task.Run(() => _context.Set<TEntity>().Where<TEntity>(predicate));
        }

        public void Remove(TEntity objModel)
        {
            _context.Set<TEntity>().Remove(objModel);
            _context.SaveChanges();
        }

        public void Update(TEntity objModel)
        {
            _context.Entry(objModel).State = EntityState.Modified;
            _context.SaveChanges();
        }


        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> filter, int skip = 0, int take = int.MaxValue, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var _resetSet = filter != null ? _context.Set<TEntity>().AsNoTracking().Where(filter).AsQueryable() : _context.Set<TEntity>().AsNoTracking().AsQueryable();

            if (include != null)
            {
                _resetSet = include(_resetSet);
            }
            if (orderBy != null)
            {
                _resetSet = orderBy(_resetSet).AsNoTracking();
            }
            _resetSet = skip == 0 ? _resetSet.Take(take) : _resetSet.Skip(skip).Take(take);

            return _resetSet.AsNoTracking();
        }

        public IQueryable<TEntity> Filter_MultipleIncludeAndOrderBy(Expression<Func<TEntity, bool>> filter, int skip = 0, int take = int.MaxValue, IEnumerable<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>> orderBy = null, IEnumerable<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>> include = null)
        {
            // Apply the filter if it's provided
            var _resetSet = filter != null ? _context.Set<TEntity>().AsNoTracking().Where(filter).AsQueryable() : _context.Set<TEntity>().AsNoTracking().AsQueryable();


            // Apply each include function if any are provided
            if (include != null)
            {
                foreach (var includeFunc in include)
                {
                    _resetSet = includeFunc(_resetSet).AsNoTracking();
                }
            }

            // Apply each order by function if any are provided
            if (orderBy != null)
            {
                foreach (var orderByFunc in orderBy)
                {
                    _resetSet = orderByFunc(_resetSet);
                }
            }

            // Apply skip and take for pagination
            _resetSet = skip == 0 ? _resetSet.Take(take) : _resetSet.Skip(skip).Take(take);

            return _resetSet.AsNoTracking();
        }
    }
}
