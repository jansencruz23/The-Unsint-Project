using System.Linq.Expressions;

namespace TheUnsintProject.Contracts
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        Task<T> GetById(object id);
        void Insert(T entity);
    }
}