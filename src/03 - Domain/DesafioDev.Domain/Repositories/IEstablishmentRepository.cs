using DesafioDev.Domain.Entities;

namespace DesafioDev.Domain.Repositories;

public interface IEstablishmentRepository
{
    Task SaveRangeAsync(ICollection<Establishment> establishment);

    Task<IEnumerable<Establishment>> GetAllAsync();
}