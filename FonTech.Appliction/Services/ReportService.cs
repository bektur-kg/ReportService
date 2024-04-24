using AutoMapper;
using FonTech.Appliction.Resources;
using FonTech.Domain.Dtos.Report;
using FonTech.Domain.Entities;
using FonTech.Domain.Enums;
using FonTech.Domain.Interfaces.Repositories;
using FonTech.Domain.Interfaces.Services;
using FonTech.Domain.Interfaces.Validations;
using FonTech.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FonTech.Appliction.Services;

public class ReportService : IReportService
{
    private readonly IBaseRepository<Report> _reportRepository;
    private readonly ILogger _logger;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IReportValidator _validator;
    private readonly IMapper _mapper;

    public ReportService(IBaseRepository<Report> reportRepository, ILogger logger,
        IBaseRepository<User> userRepository, IReportValidator validator, IMapper mapper)
    {
        _reportRepository = reportRepository;
        _logger = logger;
        _userRepository = userRepository;
        _validator = validator;
        _mapper = mapper;
    }

    ///<inheritdoc/>
    public async Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto reportDto)
    {
        var user = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.Id == reportDto.UserId);
        var report = await _reportRepository.GetAll().FirstOrDefaultAsync(r => r.Name == reportDto.Name);
        var result = _validator.ValidateCreate(report, user);

        if (!result.IsSuccess)
        {
            return new BaseResult<ReportDto>
            {
                ErrorMessage = result.ErrorMessage,
                ErrorCode = result.ErrorCode
            };
        }

        report = new Report()
        {
            Name = reportDto.Name,
            Description = reportDto.Description,
            UserId = user.Id,
        };

        await _reportRepository.CreateAsync(report);

        return new BaseResult<ReportDto>
        {
            Data = _mapper.Map<ReportDto>(report)
        };
    }

    ///<inheritdoc/>
    public async Task<BaseResult<ReportDto>> DeleteReportAsync(long id)
    {
        var report = await _reportRepository.GetAll().FirstOrDefaultAsync(r => r.Id == id);
        var result = _validator.ValidateOnNull(report);
        if (!result.IsSuccess)
        {
            return new BaseResult<ReportDto>
            {
                ErrorMessage = result.ErrorMessage,
                ErrorCode = result.ErrorCode
            };
        }

        await _reportRepository.RemoveAsync(report);

        return new BaseResult<ReportDto>
        {
            Data = _mapper.Map<ReportDto>(report)
        };
    }

    /// <inheritdoc/>
    public async Task<BaseResult<ReportDto>> GetReportByIdAsync(long id)
    {
        ReportDto? reportDto;

        reportDto = await _reportRepository
                .GetAll()
                .Where(r => r.Id == id)
                .Select(r => new ReportDto(r.Id, r.Name, r.Description, r.CreateAt.ToLongDateString()))
                .FirstOrDefaultAsync();

        if (reportDto is null)
        {
            _logger.Warning($"Report with id: {id} is not found", id);
            return new BaseResult<ReportDto>
            {
                ErrorMessage = ErrorMessage.ReportNotFound,
                ErrorCode = (int)ErrorCodes.ReportNotFound
            };
        }

        return new BaseResult<ReportDto>
        {
            Data = reportDto,
        };
    }

    /// <inheritdoc/>
    public async Task<CollectionResult<ReportDto>> GetReportsAsync(long userId)
    {
        ReportDto[] reports;

        reports = await _reportRepository
                 .GetAll()
                 .Where(r => r.Id == userId)
                 .Select(r => new ReportDto(r.Id, r.Name, r.Description, r.CreateAt.ToLongDateString()))
                 .ToArrayAsync();

        if (!reports.Any())
        {
            _logger.Warning(ErrorMessage.ReportsNotFound, reports.Length);
            return new CollectionResult<ReportDto>
            {
                ErrorMessage = ErrorMessage.ReportsNotFound,
                ErrorCode = (int)ErrorCodes.ReportsNotFound
            };
        }

        return new CollectionResult<ReportDto>
        {
            Data = reports,
            Count = reports.Length
        };
    }

    ///<inheritdoc/>
    public async Task<BaseResult<ReportDto>> UpdateReportAsync(long reportId, UpdateReportDto reportDto)
    {
        var report = await _reportRepository.GetAll().FirstOrDefaultAsync(r => r.Id == reportId);
        var result = _validator.ValidateOnNull(report);

        if (!result.IsSuccess)
        {
            return new BaseResult<ReportDto>
            {
                ErrorMessage = ErrorMessage.ReportNotFound,
                ErrorCode = (int)ErrorCodes.ReportNotFound
            };
        }

        report.Name = reportDto.Name;
        report.Description = reportDto.Description;

        await _reportRepository.UpdateAsync(report);

        return new BaseResult<ReportDto>
        {
            Data = _mapper.Map<ReportDto>(report)
        };
    }
}
