using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Notes.Queries.GetNoteList;
using Application.Notes.Queries.GetNoteDetails;

namespace WebApi.Controllers
{
	public class NoteController : BaseController
	{
		[HttpGet]
		public async Task<ActionResult<NoteListVm>> GetAll()
		{ 
			var query = new GetNoteListQuery 
			{
				UserId = UserId
			};

			var vm = await Mediator.Send(query);
			return Ok(vm);
		}

		public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
		{ 
		
		}
	}
}
