using AutoMapper;
using FonTech.Domain.Dtos.Role;
using FonTech.Domain.Entities;

namespace FonTech.Appliction.Mappings;

public class RoleMappings : Profile
{
    public RoleMappings()
    {
        CreateMap<RoleDto, Role>().ReverseMap();
        CreateMap<CreateRoleDto, Role>();
    }
}
