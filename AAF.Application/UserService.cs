using AAF.Application.Abstractions;
using AAF.Application.Features.UserFeature.Models;
using AAF.Domain.Entities;
using AAF.Domain.Enum;
using LegacyApp;
using Microsoft.Extensions.Logging;


namespace AAF.Application;

public class UserService(ILogger logger,
    IClientRepository clientRepository,
    IUserValidator validator) : IUserService
{
    public async Task<User> AddUser(UserRequestModel requestModel)
    {

        logger.LogInformation($"AddUser method commenced => The requestModel is : {requestModel}");
        try
        {
            validator.CheckValidUserName(requestModel);

            validator.CheckValidEmail(requestModel.Email);

            var dateOfBirth = requestModel.DateOfBirth;

            validator.CheckAgeGreaterThanTwentyOne(dateOfBirth);

            var client = await clientRepository.GetByIdAsync(requestModel.ClientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = requestModel.Email,
                Firstname = requestModel.Firstname,
                Surname = requestModel.Surname,
            };

            if (client.ClientStatus == ClientStatus.Gold)
            {
                // Skip credit check
                user.HasCreditLimit = false;
            }
            else if (client.ClientStatus == ClientStatus.Platinum)
            {
                // Do credit check and double credit limit
                user.HasCreditLimit = true;
                using var userCreditService = new UserCreditServiceClient();
                var creditLimit = userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
                creditLimit = creditLimit * 2;
                user.CreditLimit = creditLimit;
            }
            else
            {
                // Do credit check
                user.HasCreditLimit = true;
                using var userCreditService = new UserCreditServiceClient();
                var creditLimit = userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
                user.CreditLimit = creditLimit;
            }

            validator.CheckSufficientCreditLimit(user);

            UserDataAccess.AddUser(user);

            logger.LogInformation($"AddUser method completed => The user is : {user}");

            return user;

        }
        catch (Exception ex)
        {
            logger.LogError($"Error occoured:  {ex.Message}  - Detailed Message{ex.InnerException}");
            throw;
        }
    }


}
