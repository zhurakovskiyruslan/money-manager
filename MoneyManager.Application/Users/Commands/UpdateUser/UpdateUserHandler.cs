using MediatR;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Application.Common.Exceptions;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Users.Commands.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Guid>
{
    private readonly IWriteRepository<User> _writeRepo;
    private readonly IReadRepository<User> _readRepo;
    private readonly IUnitOfWork _uow;

    public UpdateUserHandler(IWriteRepository<User> writeRepo, 
        IReadRepository<User> readRepo, IUnitOfWork uow)
    {
        _writeRepo = writeRepo;
        _readRepo = readRepo;
        _uow = uow;
    }

    public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken ct)
    {
        var user = await _readRepo.GetByIdAsync(request.Id, ct);
        if (user == null) throw new NotFoundException("User not found.");
        user.Name = request.Name.Trim();
        user.Email = request.Email.Trim().ToLowerInvariant();
        user.BaseCurrency = request.BaseCurrency.ToUpperInvariant();
        user.TimeZone = request.TimeZone.Trim();
        
        _writeRepo.Update(user);
        await _uow.SaveChangesAsync(ct);
        return user.Id;
    }
}