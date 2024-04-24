using FonTech.Appliction.Resources;
using FonTech.Domain.Dtos;
using FonTech.Domain.Dtos.User;
using FonTech.Domain.Entities;
using FonTech.Domain.Enums;
using FonTech.Domain.Interfaces.Repositories;
using FonTech.Domain.Interfaces.Services;
using FonTech.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FonTech.Appliction.Services;

public class AuthService : IAuthService
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<UserToken> _userTokenRepository;
    private readonly ITokenService _tokenService;
    private readonly ILogger _logger;

    public AuthService(IBaseRepository<User> userRepository, ILogger logger, IBaseRepository<UserToken> userTokenRepository, 
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _logger = logger;
        _userTokenRepository = userTokenRepository;
        _tokenService = tokenService;
    }

    /// <inheritdoc/>
    public async Task<BaseResult<TokenDto>> LoginAsync(LoginUserDto dto)
    {
        var foundUser = await _userRepository.GetAll().FirstOrDefaultAsync(user => user.Login == dto.Login);
        if (foundUser == null)
        {
            return new BaseResult<TokenDto>
            {
                ErrorMessage = ErrorMessage.UserNotFound,
                ErrorCode = (int)ErrorCodes.UserNotFound
            };
        }
        if (!IsVerifyPassword(foundUser.Password, dto.Password))
        {
            return new BaseResult<TokenDto>
            {
                ErrorMessage = ErrorMessage.WrongPassword,
                ErrorCode = (int)ErrorCodes.WrongPassword
            };
        }

        var userToken = await _userTokenRepository.GetAll().FirstOrDefaultAsync(u => u.UserId == foundUser.Id);
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, foundUser.Login),
                new Claim(ClaimTypes.Role, "User"),
            };
        var accessToken = _tokenService.GenerateAccessToken(claims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        if (userToken == null)
        {
            userToken = new UserToken()
            {
                UserId = foundUser.Id,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7),
            };

            await _userTokenRepository.CreateAsync(userToken);
        }
        else
        {
            userToken.RefreshToken = refreshToken;
            userToken.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userTokenRepository.UpdateAsync(userToken);
        }

        return new BaseResult<TokenDto>
        {
            Data = new TokenDto(accessToken, refreshToken)
        };
    }

    /// <inheritdoc/>
    public async Task<BaseResult> RegisterAsync(RegisterUserDto dto)
    {
        var foundUser = await _userRepository.GetAll().FirstOrDefaultAsync(user => user.Login == dto.Login);
        if (foundUser != null)
        {
            return new BaseResult
            {
                ErrorMessage = ErrorMessage.UserAlreadyExists,
                ErrorCode = (int)ErrorCodes.UserAlreadyExists
            };
        }

        var hashedPassword = HashPassword(dto.Password);
        var newUser = new User
        {
            Password = hashedPassword,
            Login = dto.Login,
        };

        await _userRepository.CreateAsync(newUser);

        return new BaseResult();
    }

    private string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private bool IsVerifyPassword(string passwordHash, string loginPassword)
    {
        var hash = HashPassword(loginPassword);
        return hash == passwordHash;
    }
}
