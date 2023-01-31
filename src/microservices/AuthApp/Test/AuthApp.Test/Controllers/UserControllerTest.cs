using AuthApp.API.Controllers;
using AuthApp.Application.Mapper;
using AuthApp.Application.Services.Users;
using AuthApp.Application.Validators;
using AuthApp.Domain;
using AuthApp.Infra.Data.Repositories.RefreshToken;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace AuthApp.Test.Controllers;

public class UserControllerTest
{
    public UserControllerTest()
    {
        var authRequestValidator = new AuthRequestValidator();
        var registerRequestValidator = new RegisterRequestValidator();
        var logger = Substitute.For<ILogger<UserController>>();
        var mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new TokenProfile());
        }).CreateMapper();

        var userManager = Substitute.For<UserManager<User>>();
        var refreshTokenRepository = Substitute.For<IRefreshTokenRepository>();

        //var userServices = new UserServices();

        //var controller = new UserController(userServices, authRequestValidator, registerRequestValidator, mapper, logger);

        //public UserServices(UserManager<User> userManager, IRefreshTokenRepository refreshTokenRepository, IJwtServices jwtServices, IEmailSender emailSender, IConfiguration configuration)
        //{
        //    _userManager = userManager;
        //    _refreshTokenRepository = refreshTokenRepository;
        //    _jwtServices = jwtServices;
        //    _emailSender = emailSender;
        //    _configuration = configuration;
        //}
    }

    [Fact]
    public async Task Should_ThrowException_When_AgeLessThan18()
    {

    }
}
