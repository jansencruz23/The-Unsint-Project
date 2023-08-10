using TheUnsintProject.Models;

namespace TheUnsintProject.Contracts
{
    public interface IUnitOfWork
    {
        IBaseRepository<Letter> LetterRepository { get; }
        Task SaveAsync();
    }
}