using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Notes.Queries.GetNoteList;
using Application.Notes.Queries.GetNoteDetails;
using AutoMapper;
using Application.Notes.Commands.CreateNote;
using WebApi.Models;
using Application.Notes.Commands.UpdateNote;
using Application.Notes.Commands.DeleteNote;
using Microsoft.AspNetCore.Http;

namespace WebApi.Controllers
{
	[Produces("application/json")]
	[Route("api/[controller]")]
	public class NoteController : BaseController
	{
		private readonly IMapper _mapper;

		public NoteController(IMapper mapper) => _mapper = mapper;

		/// <summary>
		/// Gets the list of notes
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// GET /note
		/// </remarks>
		/// <returns>Returns NoteListVm</returns>
		/// <response code="200">Success</response>
		/// <response code="401">If the user is unauthorized</response>
		[HttpGet]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<NoteListVm>> GetAll()
		{ 
			var query = new GetNoteListQuery 
			{
				UserId = UserId
			};

			var vm = await Mediator.Send(query);
			return Ok(vm);
		}

		/// <summary>
		/// Gets the note by id
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// GET /note/B90A1E05-0634-49FB-9095-EE6F59CA59E5
		/// </remarks>
		/// <param name="id">Note id (guid)</param>
		/// <returns>Returns NoteDetailsVm</returns>
		/// <response code="200">Success</response>
		/// <response code="401">If the user is unauthorized</response>
		[HttpGet("{id}")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
		{
			var query = new GetNoteDetailsQuery
			{
				UserId = UserId,
				Id = id
			};
			var vm = await Mediator.Send(query);

			return Ok(vm);
		}

		/// <summary>
		/// Craetes the note
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// POST /note
		/// {
		///		title: "note title",
		///		details: "note details"
		/// }
		/// </remarks>
		/// <param name="createNoteDto">CreateNoteDto object</param>
		/// <returns>Returns id (guid)</returns>
		/// <response code="201">Success</response>
		/// <response code="401">If the user is unauthorized</response>
		[HttpPost]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
		{
			var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
			command.UserId = UserId;
			var noteId = await Mediator.Send(command);
			return Ok(noteId);
		}

		/// <summary>
		/// Updates the note
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// PUT /note
		/// {
		///		title: "updated note title"
		/// }
		/// </remarks>
		/// <param name="updateNoteDto">UpdateNoteDto object</param>
		/// <returns>Returns NoContext</returns>
		/// <response code="204">Success</response>
		/// <response code="401">If the user is unauthorized</response>
		[HttpPut]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<Guid>> Update([FromBody] UpdateNoteDto updateNoteDto)
		{
			var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
			command.UserId = UserId;
			await Mediator.Send(command);
			return NoContent();
		}

		/// <summary>
		/// Deletes the note by id
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// DELETE /note/88A691D8-0F81-4E06-8E4E-2485AF9C134F
		/// </remarks>
		/// <param name="id">Id of the note (guid)</param>
		/// <returns>Returns NoContext</returns>
		/// <response code="204">Success</response>
		/// <response code="401">If the user is unauthorized</response>
		[HttpDelete("{id}")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<Guid>> Delete(Guid id)
		{
			var command = new DeleteNoteCommand
			{
				Id = id,
				UserId = UserId
			};

			await Mediator.Send(command);
			return NoContent();
		}
	}
}
