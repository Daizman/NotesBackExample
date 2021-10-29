using Notes.Tests.Common;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Application.Notes.Commands.DeleteNote;
using Application.Notes.Commands.CreateNote;
using System.Threading;
using Application.Common.Exceptions;

namespace Notes.Tests.Notes.Commands
{
	public class DeleteNoteCommandHandlerTests : TestCommandBase
	{
		[Fact]
		public async Task DeleteNoteCommandHandler_Success()
		{
			// Подготовка
			var handler = new DeleteNoteCommandHandler(Context);

			// Выполнение
			await handler.Handle(new DeleteNoteCommand
			{
				Id = NotesContextFactory.NoteIdForDelete,
				UserId = NotesContextFactory.UserAId
			}, CancellationToken.None);

			// Проверка
			Assert.Null(Context.Notes.SingleOrDefault(note => 
				note.Id == NotesContextFactory.NoteIdForDelete));
		}

		[Fact]
		public async Task DeleteNoteCommandHandler_FailOnWrongId()
		{
			// Подготовка
			var handler = new DeleteNoteCommandHandler(Context);

			// Выполнение
			// Проверка
			await Assert.ThrowsAsync<NotFoundException>(async () => 
				await handler.Handle(
					new DeleteNoteCommand
					{ 
						Id = Guid.NewGuid(),
						UserId = NotesContextFactory.UserAId
					}, CancellationToken.None));
		}

		[Fact]
		public async Task DeleteNoteCommandHandler_FailOnWrongUserId()
		{
			// Подготовка
			var deleteHandler = new DeleteNoteCommandHandler(Context);
			var createHandler = new CreateNoteCommandHandler(Context);
			var noteId = await createHandler.Handle(
				new CreateNoteCommand
				{
					Title = "NoteTitle",
					UserId = NotesContextFactory.UserAId
				}, CancellationToken.None);

			// Выполнение
			// Проверка
			await Assert.ThrowsAsync<NotFoundException>(async () =>
				await deleteHandler.Handle(
					new DeleteNoteCommand
					{
						Id = noteId,
						UserId = NotesContextFactory.UserBId
					}, CancellationToken.None));
		}
	}
}
