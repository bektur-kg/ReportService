using FluentValidation;
using FonTech.Domain.Dtos.Report;

namespace FonTech.Appliction.Validations.FluentValidations.Report;

public class UpdateReportValidator : AbstractValidator<UpdateReportDto>
{
    public UpdateReportValidator()
    {
        RuleFor(r => r.Id).NotEmpty();
        RuleFor(r => r.Name).NotEmpty().MinimumLength(200);
        RuleFor(r => r.Description).NotEmpty().MinimumLength(1000);
    }
}
