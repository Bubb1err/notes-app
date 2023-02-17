using Twits.API.Services.IRepository;
using Twits.Data;
using Twits.Data.Models;

namespace Twits.API.Services
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Category> UpdateAsync(Category entity)
        {
            _db.Categories.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
