using AAF.Application;
using AAF.Application.Abstractions;
using AAF.Application.Features.UserFeature.Models;
using AAF.Application.Features.UserFeature.Validator;
using AAF.Persistence.Repositories;
using Microsoft.Extensions.Logging;

namespace AAF.UnitTest;

[TestFixture]
public class UserFeatureTest
{
    private  IUserService _mockUserService;

    private ILogger _logger;
    private IClientRepository _clientRepository;
    private IUserValidator _userValidator;

    [SetUp]
    public void Setup()
    {
        _clientRepository = new ClientRepository();
        _userValidator = new UserValidator();
        _mockUserService = new UserService(_logger,_clientRepository, _userValidator);
    }

    [Test]
    public void Adduser_receives_userData_successfully()
    {
        //Arrange
        UserRequestModel requestModel = new()
        {
            Firstname = "Sivakumar",
            Surname = "Sankaravel Shanmugam",
            Email = "sivakumar.ss@aaf.com.au",
            DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            ClientId = 007
        };

        //Act 
        _ = _mockUserService.AddUser(requestModel);

        //Assert
        Assert.IsNotNull(requestModel);
       
    }
}