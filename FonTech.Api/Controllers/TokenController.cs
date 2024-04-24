using FonTech.Appliction.Services;
using FonTech.Domain.Dtos;
using FonTech.Domain.Dtos.Report;
using FonTech.Domain.Interfaces.Services;
using FonTech.Domain.Results;
using Microsoft.AspNetCore.Mvc;

namespace FonTech.Api.Controllers;

[ApiController]
[Route("token")]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;
    
    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    /// <summary>
    ///     Refresh Token
    /// </summary>
    /// <param name="tokenDto">token dto</param>
    /// <remarks>
    /// Request to update token
    /// 
    ///     {
    ///        "accessToken": "some token",
    ///        "refreshToken": "some token",
    ///     }    
    ///     
    /// </remarks>
    [HttpPost("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<TokenDto>>> RefreshToken(TokenDto tokenDto)
    {
        var response = await _tokenService.RefreshToken(tokenDto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}
