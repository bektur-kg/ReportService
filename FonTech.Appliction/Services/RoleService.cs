using AutoMapper;
using FonTech.Appliction.Resources;
using FonTech.Domain.Dtos.Role;
using FonTech.Domain.Entities;
using FonTech.Domain.Enums;
using FonTech.Domain.Interfaces.Repositories;
using FonTech.Domain.Interfaces.Services;
using FonTech.Domain.Results;
using Microsoft.EntityFrameworkCore;

namespace FonTech.Appliction.Services;

public class RoleService
    (
        IBaseRepository<Role> roleRepository, 
        IBaseRepository<User> userRepository,
        IMapper mapper
    ) : IRoleService
{
    private readonly IBaseRepository<Role> _roleRepository = roleRepository;
    private readonly IBaseRepository<User> _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResult<RoleDto>> CreateRoleAsync(CreateRoleDto dto)
    {
        var role = _roleRepository.GetAll().FirstOrDefaultAsync(r => r.Name == dto.Name);

        if(role != null)
        {
            return new BaseResult<RoleDto>
            {
                ErrorMessage = ErrorMessage.RoleAlreadyExists,
                ErrorCode = (int)ErrorCodes.RoleAlreadyExists
            };
        }

        var newRole = _mapper.Map<Role>(dto);
        await _roleRepository.CreateAsync(newRole);

        return new BaseResult<RoleDto>
        {
            Data = _mapper.Map<RoleDto>(newRole)
        };
    }

    public async Task<BaseResult> DeleteRoleAsync(long id)
    {
        var role = await _roleRepository.GetAll().FirstOrDefaultAsync(r => r.Id == id);
        if (role == null)
        {
            return new BaseResult
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode = (int)ErrorCodes.RoleNotFound
            };
        }

        await _roleRepository.RemoveAsync(role);
        return new BaseResult();
    }

    public async Task<BaseResult> UpdateRoleAsync(RoleDto dto)
    {
        var role = await _roleRepository.GetAll().FirstOrDefaultAsync(r => r.Id == dto.Id);
        if (role == null)
        {
            return new BaseResult
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode = (int)ErrorCodes.RoleNotFound
            };
        }

        _mapper.Map(dto, role);

        return new BaseResult();
    }
}
