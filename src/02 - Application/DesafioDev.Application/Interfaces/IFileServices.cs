using DesafioDev.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace DesafioDev.Application.Interfaces
{
    public interface IFileServices
    {
        ICollection<Establishment> DesserializeValuesForEstablishment(IFormFile formFile); 
    }
}
