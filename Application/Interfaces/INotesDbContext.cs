using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Application.Interfaces
{
	public interface INotesDbContext
	{
		// интерфейс - часть приложения, а интерфейс во вне
		DbSet<Note> Notes { get; set; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);  // сохраняет изменение контекста в нашу БД
	}
}
