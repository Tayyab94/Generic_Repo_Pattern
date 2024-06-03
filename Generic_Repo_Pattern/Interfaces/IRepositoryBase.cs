using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Generic_Repo_Pattern.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        void Add(T objModel);
        void AddRange(IEnumerable<T> objModel);
        T? GetId(int id);
        Task<T?> GetIdAsync(int id);
        T? Get(Expression<Func<T, bool>> predicate);
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetList(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        int Count();
        Task<int> CountAsync();
        void Update(T objModel);
        void Remove(T objModel);
        void Dispose();



        IQueryable<T> Filter(Expression<Func<T, bool>> filter,
                                        int skip = 0,
                                        int take = int.MaxValue,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);



        IQueryable<T> Filter_MultipleIncludeAndOrderBy(Expression<Func<T, bool>> filter, int skip = 0,
         int take = int.MaxValue,
         IEnumerable<Func<IQueryable<T>, IOrderedQueryable<T>>> orderBy = null,
         IEnumerable<Func<IQueryable<T>, IIncludableQueryable<T, object>>> include = null);
    }
}
