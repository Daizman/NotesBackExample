﻿using System;
using MediatR;

namespace Application.Notes.Commands.DeleteNote
{
	public class DeleteNoteCommand: IRequest
	{
		public Guid UserId { get; set; }
		public Guid Id { get; set; }
	}
}
