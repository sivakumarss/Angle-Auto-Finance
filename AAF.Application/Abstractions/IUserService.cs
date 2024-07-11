using AAF.Application.Features.UserFeature.Models;
using AAF.Domain.Entities;

namespace AAF.Application.Abstractions;

public interface IUserService
{
    Task<User> AddUser(UserRequestModel requestModel);
}
