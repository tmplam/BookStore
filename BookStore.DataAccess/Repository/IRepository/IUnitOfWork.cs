namespace BookStore.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IGenreRepository Genre { get; }
        public IProductRepository Product { get; }
        public ICompanyRepository Company { get; }
        public IAppliccationRepository ApplicationUser { get; }

        Task CreateTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task SaveChangesAsync();
    }
}
