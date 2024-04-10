using AutoMapper;
using FonTech.Domain.Dtos.Report;
using FonTech.Domain.Entities;

namespace FonTech.Appliction.Mappings;

public class ReportMapping : Profile
{
    public ReportMapping()
    {
        CreateMap<Report, ReportDto>().ReverseMap();
    }
}
