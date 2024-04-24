using FonTech.Domain.Dtos.Report;
using FonTech.Domain.Dtos.Role;
using FonTech.Domain.Interfaces.Services;
using FonTech.Domain.Results;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace FonTech.Api.Controllers;

[ApiController]
[Route("roles")]
[Consumes(MediaTypeNames.Application.Json)]
public class RoleController(IRoleService roleService) : ControllerBase
{
    private readonly IRoleService _roleService = roleService;

    /// <summary>
    ///     Create Role
    /// </summary>
    /// <param name="dto">Role DTO</param>
    /// <response code="200">If role added successfully</response>
    /// <response code="400">If role already exists</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> CreateRole(RoleDto dto)
    {
        var response = await _roleService.CreateRoleAsync(dto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    ///     Delete Role
    /// </summary>
    /// <param name="id">Role id</param>
    /// <response code="200">If role deleted successfully</response>
    /// <response code="400">If role is not found</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> DeleteRole(long id)
    {
        var response = await _roleService.DeleteRoleAsync(id);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    ///     Update Role
    /// </summary>
    /// <param name="dto">Role DTO</param>
    /// <response code="200">If role updated successfully</response>
    /// <response code="400">If role is not found</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> UpdateRole(RoleDto dto)
    {
        var response = await _roleService.UpdateRoleAsync(dto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}
