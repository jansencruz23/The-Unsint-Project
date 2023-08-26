using Microsoft.EntityFrameworkCore;

namespace TheUnsintProject.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }

        public PaginatedList(IEnumerable<T> items, 
            int count, int page, int size) 
        {
            Page = page;
            TotalPages = (int)Math.Ceiling(count / (double)size);

            AddRange(items);
        }

        public bool HasPreviousPage => Page > 1;
        public bool HasNextPage => Page < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source,
            int page, int size)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PaginatedList<T>(items, count, page, size);
        }

        public static PaginatedList<T> Create(IEnumerable<T> source,
            int page, int size)
        {
            var count = source.Count();
            var items = source.Skip((page - 1) * size)
                .Take(size)
                .ToList();

            return new PaginatedList<T>(items, count, page, size);
        }
    }
}