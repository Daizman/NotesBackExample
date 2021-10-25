using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Application.Interfaces;
using Application.Common.Exceptions;
using Domain;

namespace Application.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommandHandler: IRequestHandler<DeleteNoteCommand>
    {
        private readonly INotesDbContext _dbContext;

        public DeleteNoteCommandHandler(INotesDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            // Unit - тип означающий пустой ответ

            var entity = await _dbContext.Notes.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity is null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            _dbContext.Notes.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
