using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore.Storage;

#nullable disable

namespace BookStore.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposed;

        private readonly ApplicationDbContext dbContext;

        public IGenreRepository Genre { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company { get; private set; }

        private IDbContextTransaction transaction;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            Genre = new GenreRepository(dbContext);
            Product = new ProductRepository(dbContext);
            Company = new CompanyRepository(dbContext);
        }

        public async Task CreateTransactionAsync()
        {
            transaction = await dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await transaction.RollbackAsync();
            transaction.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Disposing of the Context Object
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
