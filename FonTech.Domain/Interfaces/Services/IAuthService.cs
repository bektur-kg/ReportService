using FonTech.Domain.Dtos;
using FonTech.Domain.Dtos.User;
using FonTech.Domain.Results;

namespace FonTech.Domain.Interfaces.Services;

/// <summary>
/// Service for authentication or authorization
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// User register
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult> RegisterAsync(RegisterUserDto dto);

    /// <summary>
    /// Login user
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<TokenDto>> LoginAsync(LoginUserDto dto);
}
