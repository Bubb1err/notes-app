using Twits.API.Services.IRepository;
using Twits.Data;
using Twits.Data.Models;

namespace Twits.API.Services
{
    public class NotesRepository : Repository<Note>, INotesRepository
    {
        private readonly ApplicationDbContext _db;
        public NotesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Note> UpdateAsync(Note entity)
        {
            entity.LastModifiedDate = DateTime.Now;
            _db.Notes.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
