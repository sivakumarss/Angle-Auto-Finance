using AAF.Domain.Entities;

namespace AAF.Application.Abstractions;

public interface IClientRepository
{
    Task<Client> GetByIdAsync(int id);
}
