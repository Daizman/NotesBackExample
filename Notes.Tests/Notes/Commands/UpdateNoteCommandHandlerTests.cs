using Notes.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Application.Notes.Commands.UpdateNote;
using Application.Notes.Commands.CreateNote;

namespace Notes.Tests.Notes.Commands
{
	public class UpdateNoteCommandHandlerTests : TestCommandBase
	{
		[Fact]
		public async Task UpdateNoteCommandHandler_Success()
		{
			// Подготовка
			var handler = new UpdateNoteCommandHandler(Context);
			var updateTitle = "neww title";

			// Выполнение
			await handler.Handle(new UpdateNoteCommand
			{
				Id = NotesContextFactory.NoteIdForUpdate,
				UserId = NotesContextFactory.UserBId,
				Title = updateTitle
			}, CancellationToken.None);

			// Проверка
		}
	}
}
