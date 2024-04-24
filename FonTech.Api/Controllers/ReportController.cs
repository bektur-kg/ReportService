using Asp.Versioning;
using FonTech.Domain.Dtos.Report;
using FonTech.Domain.Interfaces.Services;
using FonTech.Domain.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FonTech.Api.Controllers;

[Authorize(Roles = "User")]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/reports")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    /// <summary>
    ///     Get all Reports
    /// </summary>
    /// <param name="id">Report Id</param>
    /// <response code="200">If report is found</response>
    /// <response code="400">If report is not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> GetReport(long id)
    {
        var response = await _reportService.GetReportByIdAsync(id);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    ///     Get user reports
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <response code="200">If reports are found successfully</response>
    /// <response code="400">If user is not found</response>
    [HttpGet("reports/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> GetUserReports(long userId)
    {
        var response = await _reportService.GetReportsAsync(userId);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    ///     Delete Report
    /// </summary>
    /// <param name="id">Report Id</param>
    /// <response code="200">If report is deleted successfully</response>
    /// <response code="400">If report is not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> DeleteReport(long id)
    {
        var response = await _reportService.DeleteReportAsync(id);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    ///     Create Report
    /// </summary>
    /// <param name="reportDto"></param>
    /// <remarks>
    /// Request to create report
    /// 
    ///     {
    ///        "name": "Report #1",
    ///        "description": "test report",
    ///        "userId": 1
    ///     }
    ///         
    /// </remarks>
    /// <response code="200">If report is deleted successfully</response>
    /// <response code="400">If report is not found</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> CreateReport(CreateReportDto reportDto)
    {
        var response = await _reportService.CreateReportAsync(reportDto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    ///     Update report
    /// </summary>
    /// <param name="id">Report Id</param>
    /// <param name="reportDto">Updated report</param>
    /// <remarks>
    /// Request to update report
    /// 
    ///     {
    ///        "name": "Updated Report #1",
    ///        "description": "updated report description",
    ///     }    
    ///     
    /// </remarks>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> UpdateReport(long id, UpdateReportDto reportDto)
    {
        var response = await _reportService.UpdateReportAsync(id, reportDto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}
