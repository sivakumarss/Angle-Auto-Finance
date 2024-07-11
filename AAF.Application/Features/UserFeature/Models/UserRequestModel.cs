namespace AAF.Application.Features.UserFeature.Models;

public class UserRequestModel
{
    public string Firstname { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }

    public int ClientId { get; set; }
}
