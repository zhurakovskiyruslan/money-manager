using MediatR;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Application.Common.Exceptions;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Users.Commands.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IWriteRepository<User> _write;
    private readonly IReadRepository<User> _read;
    private readonly IUnitOfWork _uow;

    public DeleteUserHandler(IWriteRepository<User> write, IReadRepository<User> read, IUnitOfWork uow)
    {
        _write = write;
        _read = read;
        _uow = uow;
    }
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken ct)
    {
        var user = await _read.GetByIdAsync(request.Id, ct);
        if (user == null) throw new NotFoundException($"User not found");
        _write.Remove(user);
        await _uow.SaveChangesAsync(ct);
        return Unit.Value;
    }
}