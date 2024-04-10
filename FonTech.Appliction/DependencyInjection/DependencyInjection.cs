using FluentValidation;
using FonTech.Appliction.Mappings;
using FonTech.Appliction.Services;
using FonTech.Appliction.Validations;
using FonTech.Appliction.Validations.FluentValidations.Report;
using FonTech.Domain.Dtos.Report;
using FonTech.Domain.Interfaces.Services;
using FonTech.Domain.Interfaces.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace FonTech.Appliction.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ReportMapping));
        services.InitServices();
    }

    private static void InitServices(this IServiceCollection services)
    {
        services.AddScoped<IReportValidator, ReportValidator>();
        services.AddScoped<IValidator<CreateReportDto>, CreateReportValidator>();
        services.AddScoped<IValidator<UpdateReportDto>, UpdateReportValidator>();
        services.AddScoped<IReportService, ReportService>();
    }
}
