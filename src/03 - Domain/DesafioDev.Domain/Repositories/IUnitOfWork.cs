namespace DesafioDev.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IEstablishmentRepository EstablishmentRepository { get; set; }

        Task CommitAsync();
    }
}
