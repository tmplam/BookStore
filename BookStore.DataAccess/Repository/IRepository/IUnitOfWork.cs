namespace BookStore.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IGenreRepository Genre { get; }

        Task SaveChangesAsync();
    }
}
