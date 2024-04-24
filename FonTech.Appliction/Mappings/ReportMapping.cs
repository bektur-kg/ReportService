using AutoMapper;
using FonTech.Domain.Dtos.Report;
using FonTech.Domain.Entities;

namespace FonTech.Appliction.Mappings;

public class ReportMapping : Profile
{
    public ReportMapping()
    {
        CreateMap<Report, ReportDto>()
            .ForCtorParam(ctorParamName: "Id", (m) => m.MapFrom(s => s.Id))
            .ForCtorParam(ctorParamName: "Name", (m) => m.MapFrom(s => s.Name))
            .ForCtorParam(ctorParamName: "Description", (m) => m.MapFrom(s => s.Description))
            .ForCtorParam(ctorParamName: "DateCreated", (m) => m.MapFrom(s => s.CreateAt))
            .ReverseMap();
    }
}
