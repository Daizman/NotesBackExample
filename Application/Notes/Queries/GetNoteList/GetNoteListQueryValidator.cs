using System;
using FluentValidation;

namespace Application.Notes.Queries.GetNoteList
{
	public class GetNoteListQueryValidator : AbstractValidator<GetNoteListQuery>
	{
		public GetNoteListQueryValidator()
		{
			RuleFor(notes => notes.UserId).NotEqual(Guid.Empty);
		}
	}
}
