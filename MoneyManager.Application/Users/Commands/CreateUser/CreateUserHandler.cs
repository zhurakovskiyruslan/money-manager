using MediatR;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Users.Commands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IWriteRepository<User> _repo;
    private readonly IUnitOfWork _uow;

    public CreateUserHandler(IWriteRepository<User> repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken ct)
    {
        var user = new User
        {
            Name = request.Name.Trim(),
            Email = request.Email.Trim().ToLowerInvariant(),
            BaseCurrency = request.BaseCurrency.ToUpperInvariant().Trim(),
            TimeZone = request.TimeZone.Trim(),
        };
        await _repo.AddAsync(user, ct);
        await _uow.SaveChangesAsync(ct);
        return user.Id;
    }
}