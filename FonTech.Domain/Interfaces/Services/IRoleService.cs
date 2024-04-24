using FonTech.Domain.Dtos.Role;
using FonTech.Domain.Results;

namespace FonTech.Domain.Interfaces.Services;

/// <summary>
/// Service for Role manipulation
/// </summary>
public interface IRoleService
{
    /// <summary>
    /// Create Role
    /// </summary>
    /// <param name="dto">Role DTO</param>
    Task<BaseResult<RoleDto>> CreateRoleAsync(RoleDto dto);

    /// <summary>
    /// Delete Role
    /// </summary>
    /// <param name="dto">Role id</param>
    Task<BaseResult> DeleteRoleAsync(long id);

    /// <summary>
    /// Update Role
    /// </summary>
    /// <param name="dto">Role DTO</param>
    Task<BaseResult> UpdateRoleAsync(RoleDto dto);
}
