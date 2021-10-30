using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Notes.Tests.Common;
using Persistent;
using Xunit;
using Shouldly;
using Application.Notes.Queries.GetNoteList;

namespace Notes.Tests.Notes.Queries
{
	[Collection("QueryCollection")]
	public class GetNoteListQueryHandlerTests
	{
		private readonly NotesDbContext _context;
		private readonly IMapper _mapper;

		public GetNoteListQueryHandlerTests(QueryTestFixture fixture) => (_context, _mapper)
			= (fixture.Context, fixture.Mapper);

		[Fact]
		public async Task GetNoteListQueryHandler_Success()
		{
			// Подготовка
			var handler = new GetNoteListQueryHandler(_context, _mapper);

			// Выполнение
			var result = await handler.Handle(
				new GetNoteListQuery
				{
					UserId = NotesContextFactory.UserBId
				}, CancellationToken.None);

			// Проверка
			result.ShouldBeOfType<NoteListVm>();
			result.Notes.Count.ShouldBe(2);
		}
	}
}
