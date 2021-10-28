using System;
using FluentValidation;

namespace Application.Notes.Commands.DeleteNote
{
	public class DeleteNoteValidator : AbstractValidator<DeleteNoteCommand>
	{
		public DeleteNoteValidator()
		{
			RuleFor(deleteNoteCommand => deleteNoteCommand.Id).NotEqual(Guid.Empty);
			RuleFor(deleteNoteCommand => deleteNoteCommand.UserId).NotEqual(Guid.Empty);
		}
	}
}
