using FonTech.Domain.Entities;
using FonTech.Domain.Results;

namespace FonTech.Domain.Interfaces.Validations;

public interface IReportValidator : IBaseValidator<Report>
{
    /// <summary>
    /// Checks for report with same Name in Database, if there's such - cannot create same report
    /// Checks for existence of user by UserId in Database
    /// </summary>
    /// <param name="report"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    BaseResult ValidateCreate(Report report, User user);
}
