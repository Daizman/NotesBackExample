using System;
using AutoMapper;
using Application.Interfaces;
using Application.Common.Mapping;
using Persistent;
using Xunit;

namespace Notes.Tests.Common
{
	public class QueryTestFixture : IDisposable
	{
		public NotesDbContext Context;
		public IMapper Mapper;

		public QueryTestFixture()
		{
			Context = NotesContextFactory.Create();
			var configurationProvider = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new AssemblyMappingProfile(
					typeof(INotesDbContext).Assembly));
			});
			Mapper = configurationProvider.CreateMapper();
		}

		public void Dispose()
		{
			NotesContextFactory.Destroy(Context);
		}
	}

	[CollectionDefinition("QueryCollection")]
	public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
