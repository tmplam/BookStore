using BookStore.Models;

namespace BookStore.DataAccess.Repository.IRepository
{
    public interface IAppliccationRepository : IRepository<ApplicationUser>
    {
        void Update(ApplicationUser applicationUser);
    }
}
