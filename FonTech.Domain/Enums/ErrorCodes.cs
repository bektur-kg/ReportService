namespace FonTech.Domain.Enums;

public enum ErrorCodes
{
    ReportsNotFound = 0,
    ReportNotFound = 1,
    ReportAlreadyExists = 2,

    UserNotFound = 11,
    UserAlreadyExists = 12,
    WrongPassword = 13,
    UserUnauthorizedAccess = 14,

    RoleAlreadyExists = 21,
    RoleNotFound = 22,

    InternalServerError = 10,
}
