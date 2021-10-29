using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Application.Notes.Commands.CreateNote;
using Notes.Tests.Common;
using System.Threading;

namespace Notes.Tests.Notes.Commands
{
	// Тест создания заметки
	public class CreateNoteCommandHandlerTests : TestCommandBase
	{
		// помечает метод, который во время прогона тестов должен быть вызван
		[Fact]
		public async Task CreatNoteCommandHandler_Success()
		{
			// Подготовка
			var handler = new CreateNoteCommandHandler(Context);
			var noteName = "note name";
			var noteDetails = "note details";

			// Выполнение
			var noteId = await handler.Handle(
				new CreateNoteCommand
				{
					Title = noteName,
					Details = noteDetails,
					UserId = NotesContextFactory.UserAId
				},
				CancellationToken.None);

			// Проверка
			Assert.NotNull(
				await Context.Notes.SingleOrDefaultAsync(note =>
					note.Id == noteId && note.Title == noteName && note.Details == noteDetails));
		}
	}
}
