using Generic_repo.Models;
using Generic_Repo_Pattern.Interfaces;
using Generic_Repo_Pattern.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Generic_Repo_Pattern.Repositories
{
    public class ProductsRepository : IGenericRepository<Product>, IDisposable
    {
        private readonly AppDbContext _context;

        public ProductsRepository(AppDbContext context)
        {
            this._context = context;
        }
        public void Add(in Product sender)
        {
            _context.Add(sender).State = EntityState.Added;
        }

        protected virtual void Dispose(bool disposing)
        {
            //if (!disposing)
            //{

            //}
            if (disposing)
            {
                _context.Dispose();
            }

            disposing = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Task<List<Product>> GetAllAsync()
        {
            return _context.Products.ToListAsync();
        }

        public Product GetById(int id)
        {
            return _context.Products.Find(id);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public Product GetByIdWithIncludes(int id)
        {
            return _context.Products.Include(x => x.Category)
             .FirstOrDefault(x => x.ProductId == id);
        }

        public async Task<Product> GetByIdWithIncludesAsync(int id)
        {
            return await _context.Products.Include(x => x.Category)
           .FirstOrDefaultAsync(x => x.ProductId == id);
        }

        public bool Remove(int id)
        {
            var product = _context.Products.Find(id);
            if (product is { })
            {
                _context.Products.Remove(product);
                return true;
            }

            return false;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Product Select(Expression<Func<Product, bool>> predicate)
        {
            var data = _context.Products.Where(predicate).FirstOrDefault()!;

            return data;
        }

        public async Task<Product> SelectAsync(Expression<Func<Product, bool>> predicate)
        {
           return await _context.Products.Where(predicate).FirstAsync()!;
        }

        public void Update(in Product sender)
        {
            _context.Update(sender).State = EntityState.Modified;

        }
    }
}
