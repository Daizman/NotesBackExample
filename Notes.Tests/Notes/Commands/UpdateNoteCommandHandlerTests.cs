using Notes.Tests.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Application.Notes.Commands.UpdateNote;
using Application.Notes.Commands.CreateNote;
using Application.Common.Exceptions;

namespace Notes.Tests.Notes.Commands
{
	public class UpdateNoteCommandHandlerTests : TestCommandBase
	{
		[Fact]
		public async Task UpdateNoteCommandHandler_Success()
		{
			// Подготовка
			var handler = new UpdateNoteCommandHandler(Context);
			var updateTitle = "new title";

			// Выполнение
			await handler.Handle(new UpdateNoteCommand
			{
				Id = NotesContextFactory.NoteIdForUpdate,
				UserId = NotesContextFactory.UserBId,
				Title = updateTitle
			}, CancellationToken.None);

			// Проверка
			Assert.NotNull(await Context.Notes.SingleOrDefaultAsync(note =>
				note.Id == NotesContextFactory.NoteIdForUpdate &&
				note.Title == updateTitle));
		}

		[Fact]
		public async Task UpdateNoteCommandHandler_FailOnWrongId()
		{
			// Подготовка
			var handler = new UpdateNoteCommandHandler(Context);

			// Выполнение
			// Проверка
			await Assert.ThrowsAsync<NotFoundException>(async () =>
				await handler.Handle(
					new UpdateNoteCommand
					{
						Id = Guid.NewGuid(),
						UserId = NotesContextFactory.UserAId
					}, CancellationToken.None));
		}

		[Fact]
		public async Task UpdateNoteCommandHandler_FailOnWrongUserId()
		{
			// Подготовка
			var handler = new UpdateNoteCommandHandler(Context);

			// Выполнение
			// Проверка
			await Assert.ThrowsAsync<NotFoundException>(async ()=>
				await handler.Handle(
					new UpdateNoteCommand
					{
						Id = NotesContextFactory.NoteIdForUpdate,
						UserId = NotesContextFactory.UserAId
					}, CancellationToken.None));
		}
	}
}
