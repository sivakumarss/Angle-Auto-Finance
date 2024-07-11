using AAF.Application.Abstractions;
using AAF.Application.Features.UserFeature.Models;
using AAF.Domain.Entities;

namespace AAF.Application.Features.UserFeature.Validator;

public class UserValidator : IUserValidator
{
    public void CheckValidUserName(UserRequestModel requestModel)
    {
        if (string.IsNullOrEmpty(requestModel.Firstname) || string.IsNullOrEmpty(requestModel.Surname))
        {
            throw new InvalidOperationException("user firstname / surname is required ");
        }
    }

    public void CheckValidEmail(string email)
    {
        if (!email.Contains("@") && !email.Contains("."))
        {
            throw new InvalidOperationException("user email is invalid ");
        }
    }

    public void  CheckAgeGreaterThanTwentyOne(DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        int age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

        if (age < 21)
        {
            throw new InvalidOperationException("user should be older than 21 years");
        }
    }

    public void CheckSufficientCreditLimit(User user)
    {
        if (user.HasCreditLimit && user.CreditLimit < 500)
        {
            throw new InvalidOperationException("insufficient credit limit");
        }
    }
}
