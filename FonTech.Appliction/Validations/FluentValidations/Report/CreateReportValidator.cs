using FluentValidation;
using FonTech.Domain.Dtos.Report;

namespace FonTech.Appliction.Validations.FluentValidations.Report;

public class CreateReportValidator : AbstractValidator<CreateReportDto>
{
    public CreateReportValidator()
    {
        RuleFor(r => r.Name).NotEmpty().MaximumLength(200);
        RuleFor(r => r.Description).NotEmpty().MaximumLength(1000);
    }
}
