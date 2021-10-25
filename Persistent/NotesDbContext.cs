using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Domain;
using Persistent.EntityTypeConfigurations;

namespace Persistent
{
	public class NotesDbContext : DbContext, INotesDbContext
	{

		public DbSet<Note> Notes { get; set; }

		public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder) 
		{
			builder.ApplyConfiguration(new NoteConfig());
			base.OnModelCreating(builder);
		}
	}
}
