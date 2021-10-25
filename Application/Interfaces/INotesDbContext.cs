using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Application.Interfaces
{
    public interface INotesDbContext
    {
        DbSet<Note> Notes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
