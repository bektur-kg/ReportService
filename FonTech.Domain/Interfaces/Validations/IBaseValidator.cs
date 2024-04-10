using FonTech.Domain.Results;

namespace FonTech.Domain.Interfaces.Validations;

public interface IBaseValidator<T> where T : class
{
    BaseResult ValidateOnNull(T model);
}
