using FonTech.Domain.Dtos;
using FonTech.Domain.Dtos.Report;
using FonTech.Domain.Dtos.User;
using FonTech.Domain.Interfaces.Services;
using FonTech.Domain.Results;
using Microsoft.AspNetCore.Mvc;

namespace FonTech.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Register user
    /// </summary>
    /// <param name="dto">register data</param>
    /// <remarks>
    /// Request to register user
    /// 
    ///     {
    ///        "Login": "bektur",
    ///        "Password": "1B2E3K4T5U6R7",
    ///     }    
    ///     
    /// </remarks>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult>> Register(RegisterUserDto dto)
    {
        var response = await _authService.RegisterAsync(dto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Login user
    /// </summary>
    /// <param name="dto">Login data</param>
    /// <remarks>
    /// Request to login user
    /// 
    ///     {
    ///        "Login": "bektur",
    ///        "Password": "1B2E3K4T5U6R7",
    ///     }    
    ///     
    /// </remarks>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<TokenDto>>> Login(LoginUserDto dto)
    {
        var response = await _authService.LoginAsync(dto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}
