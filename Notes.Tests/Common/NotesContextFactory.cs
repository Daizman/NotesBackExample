using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistent;
using Domain;

namespace Notes.Tests.Common
{
	public static class NotesContextFactory
	{
		public static Guid UserAId = Guid.NewGuid();
		public static Guid UserBId = Guid.NewGuid();

		public static Guid NoteIdForDelete = Guid.NewGuid();
		public static Guid NoteIdForUpdate = Guid.NewGuid();

		public static NotesDbContext Create()
		{
			var options = new DbContextOptionsBuilder<NotesDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString())
				.Options;

			var context = new NotesDbContext(options);
			context.Database.EnsureCreated();
			context.Notes.AddRange(
				new Note
				{ 
					CreationDate = DateTime.Today,
					Details = "Details1",
					EditDate = null,
					Id = Guid.Parse("4C880E79-6887-4832-8089-6B14178C8B75"),
					Title = "Title1",
					UserId = UserAId
				},
				new Note
				{
					CreationDate = DateTime.Today,
					Details = "Details2",
					EditDate = null,
					Id = Guid.Parse("3B35EBB7-ABB7-48EE-9582-188FB802EA6F"),
					Title = "Title2",
					UserId = UserBId
				},
				new Note
				{ 
					CreationDate = DateTime.Today,
					Details = "Details3",
					EditDate = null,
					Id = NoteIdForDelete,
					Title = "Title3",
					UserId = UserAId
				},
				new Note
				{ 
					CreationDate = DateTime.Today,
					Details = "Details4",
					EditDate = null,
					Id = NoteIdForUpdate,
					Title = "Title4",
					UserId = UserBId
				}
			);

			context.SaveChanges();
			return context;
		}

		public static void Destroy(NotesDbContext context)
		{
			context.Database.EnsureDeleted();
			context.Dispose();
		}
	}
}
