using FonTech.Appliction.Resources;
using FonTech.Domain.Entities;
using FonTech.Domain.Enums;
using FonTech.Domain.Interfaces.Validations;
using FonTech.Domain.Results;

namespace FonTech.Appliction.Validations;

public class ReportValidator : IReportValidator
{
    public BaseResult ValidateCreate(Report report, User user)
    {
        if (report != null)
        {
            return new BaseResult
            {
                ErrorMessage = ErrorMessage.ReportAlreadyExists,
                ErrorCode = (int)ErrorCodes.ReportAlreadyExists
            };
        }

        if(user is null)
        {
            return new BaseResult
            {
                ErrorMessage = ErrorMessage.UserNotFound,
                ErrorCode = (int)ErrorCodes.UserNotFound
            };
        }

        return new BaseResult();
    }

    public BaseResult ValidateOnNull(Report model)
    {
        if(model is null)
        {
            return new BaseResult
            {
                ErrorMessage = ErrorMessage.ReportNotFound,
                ErrorCode = (int)ErrorCodes.ReportNotFound
            };
        }

        return new BaseResult();
    }
}
