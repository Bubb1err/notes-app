using Twits.Data.Models;

namespace Twits.API.Services.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> UpdateAsync(Category note);
    }
}
