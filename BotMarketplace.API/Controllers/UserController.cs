using BotMarketplace.API.DTOs;
using BotMarketplace.API.DTOs.Users;
using BotMarketplace.Core.Models;
using BotMarketplace.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BotMarketplace.API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class UserController : BasicController<UserBaseDTO, User, IUserService>
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _service;

    public UserController(ILogger<UserController> logger, IUserService service) : base(service)
    {
        _logger = logger;
        _service = service;
    }

    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="Name">The users name</param>
    /// <param name="Email">The users email</param>
    /// <returns>A newly created user</returns>
    /// <remarks>
    /// 
    ///     POST /api/user
    ///     {
    ///         "Name": "the user name",
    ///         "Email": "user@email.com"
    ///     }
    /// 
    /// </remarks>
    /// <response code="201">Returns the newly created user</response>
    /// <response code="400">If the required values are not sent or are invalid</response>
    public override async Task<IActionResult> Create(UserBaseDTO dto)
    {
        return await base.Create(dto);
    }

    /// <summary>
    /// Maps a TransactionBaseDTO to a Transaction model
    /// </summary>
    /// <param name="dto">The TransactionBaseDTO containing the data for the transaction</param>
    /// <returns></returns>
    protected override User MapToModel(UserBaseDTO dto)
    {
        var user = new User(dto.Name, dto.Email);

        return user;
    }

    protected override void UpdateModel(User model, UserBaseDTO dto)
    {
        model.Name = dto.Name;
        model.Email = dto.Email;
    }
}
