using DesafioDev.Domain.Repositories;

namespace DesafioDev.Infra.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IEstablishmentRepository EstablishmentRepository { get; set; }

    public UnitOfWork(ApplicationDbContext context, IEstablishmentRepository establishmentRepository)
    {
        _context = context;
        EstablishmentRepository = establishmentRepository;
    }

    public async Task CommitAsync()
        => await _context.SaveChangesAsync();
}
