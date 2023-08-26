using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TheUnsintProject.Contracts;
using TheUnsintProject.Data;

namespace TheUnsintProject.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        private readonly TUPDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(TUPDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> search = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (search != null)
            {
                query = query.Where(search);
            }

            foreach (var include in includeProperties.Split(
                new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                return await orderBy(query)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                return await query
                    .AsNoTracking()
                    .ToListAsync();
            }
        }

        public virtual async Task<T> GetById(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }
    }
}