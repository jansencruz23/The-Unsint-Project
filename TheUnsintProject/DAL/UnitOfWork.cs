using TheUnsintProject.Contracts;
using TheUnsintProject.Data;
using TheUnsintProject.Models;
using TheUnsintProject.Repositories;

namespace TheUnsintProject.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TUPDbContext _context;
        private IBaseRepository<Letter> letterRepository;

        public IBaseRepository<Letter> LetterRepository =>
            letterRepository ??= new BaseRepository<Letter>(_context);

        public UnitOfWork(TUPDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}