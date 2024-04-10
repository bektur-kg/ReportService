using FonTech.Domain.Dtos.Report;
using FonTech.Domain.Results;

namespace FonTech.Domain.Interfaces.Services;

/// <summary>
/// Service responsible for work with Domain Part of Report
/// </summary>
public interface IReportService
{
    /// <summary>
    /// Get all Reports of Users
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<CollectionResult<ReportDto>> GetReportsAsync(long userId);

    /// <summary>
    /// Get Report by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> GetReportByIdAsync(long id);

    /// <summary>
    /// Create Report with basic parameters
    /// </summary>
    /// <param name="reportDto"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto reportDto);

    /// <summary>
    /// Delete Report by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> DeleteReportAsync(long id);

    /// <summary>
    /// Update Report
    /// </summary>
    /// <param name="reportDto"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto reportDto);
}
