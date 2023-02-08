using Twits.Data.Models;

namespace Twits.API.Services.IRepository
{
    public interface INotesRepository : IRepository<Note>
    {
        Task<Note> UpdateAsync(Note note);
    }
}
