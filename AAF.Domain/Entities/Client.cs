using AAF.Domain.Enum;

namespace AAF.Domain.Entities;

public class Client
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ClientStatus ClientStatus { get; set; }
}
