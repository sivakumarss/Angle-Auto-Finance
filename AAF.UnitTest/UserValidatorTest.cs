using AAF.Application;
using AAF.Application.Abstractions;
using AAF.Application.Features.UserFeature.Models;
using AAF.Application.Features.UserFeature.Validator;
using AAF.Domain.Entities;
using AAF.Persistence.Repositories;
using Microsoft.Extensions.Logging;

namespace AAF.UnitTest;

[TestFixture]
public class UserValidatorTest
{

    private IUserValidator _userValidator;

    [SetUp]
    public void Setup()
    {
        _userValidator = new UserValidator();
    }

    [Test]
    public void CheckValidUserNameAndEmail()
    {
        //Arrange
        var requestModel = GetMockUser();

        //Act  & //Assert
        Assert.DoesNotThrow(() => _userValidator.CheckValidUserName(requestModel), "Valid UserName");
        Assert.DoesNotThrow(() => _userValidator.CheckValidEmail(requestModel.Email), "Valid UserName");

    }


    [Test]
    public void Throws_Exception_ForInValidUserName()
    {
        //Arrange
        var requestModel = GetMockUser();
        requestModel.Surname = string.Empty;

        //Act  & //Assert
        Assert.Throws<InvalidOperationException>(() => _userValidator.CheckValidUserName(requestModel));
        
    }

    [TestCase("")]
    [TestCase("aaf")]
    public void Throws_Exception_ForInValidEmail(string email)
    {
         //Act  & //Assert
        Assert.Throws<InvalidOperationException>(() => _userValidator.CheckValidEmail(email));

    }

    [Test]
    public void Throws_Exception_ForAgeLesserThanTwentyOne()
    {
        //Arrange
        var requestModel = GetMockUser();
        requestModel.DateOfBirth = new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        //Act  & //Assert
        Assert.Throws<InvalidOperationException>(() => _userValidator.CheckAgeGreaterThanTwentyOne(requestModel.DateOfBirth));

    }


    [Test]
    public void Throws_Exception_ForInSufficientCreditLimit()
    {
        //Arrange
        User user = new()
        {
            HasCreditLimit = true,
            CreditLimit = 450
        };

        //Act  & //Assert
        Assert.Throws<InvalidOperationException>(() => _userValidator.CheckSufficientCreditLimit(user));

    }


    #region Private mock methods

    private UserRequestModel GetMockUser()
    {
        UserRequestModel requestModel = new()
        {
            Firstname = "Sivakumar",
            Surname = "Sankaravel Shanmugam",
            Email = "sivakumar.ss@aaf.com.au",
            DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            ClientId = 007
        };

        return requestModel;
    }
    #endregion
}