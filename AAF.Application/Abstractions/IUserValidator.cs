using AAF.Application.Features.UserFeature.Models;
using AAF.Domain.Entities;

namespace AAF.Application.Abstractions;

public interface IUserValidator
{
    void CheckValidUserName(UserRequestModel requestModel);
    void CheckValidEmail(string email);
    void CheckAgeGreaterThanTwentyOne(DateTime dateOfBirth);
    void CheckSufficientCreditLimit(User user);
}
