using BotMarketplace.API.DTOs;
using BotMarketplace.API.DTOs.Users;
using BotMarketplace.Core.Models;
using BotMarketplace.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BotMarketplace.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController : BasicController<UserBaseDTO, User, IUserService>
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _service;

    public UserController(ILogger<UserController> logger, IUserService service) : base(service)
    {
        _logger = logger;
        _service = service;
    }

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