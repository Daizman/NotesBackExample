using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Xunit;
using Persistent;
using Notes.Tests.Common;
using Shouldly;
using Application.Notes.Queries.GetNoteDetails;

namespace Notes.Tests.Notes.Queries
{
	[Collection("QueryCollection")]
	public class GetNoteDetailsQueryHandlerTests
	{
		private readonly NotesDbContext _context;
		private readonly IMapper _mapper;

		public GetNoteDetailsQueryHandlerTests(QueryTestFixture fixture)
		{
			_context = fixture.Context;
			_mapper = fixture.Mapper;
		}

		[Fact]
		public async Task GetNoteDetailsQueryHandler_Success()
		{
			// Подготовка
			var handler = new GetNoteDetailsQueryHandler(_context, _mapper);

			// Выполнение
			var result = await handler.Handle(
				new GetNoteDetailsQuery
				{
					UserId = NotesContextFactory.UserBId,
					Id = Guid.Parse("3B35EBB7-ABB7-48EE-9582-188FB802EA6F")
				}, CancellationToken.None);

			// Проверка
			result.ShouldBeOfType<NoteDetailsVm>();
			result.Title.ShouldBe("Title2");
			result.CreationDate.ShouldBe(DateTime.Today);
		}
	}
}
