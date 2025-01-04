using AutoMapper;
using LeaveWise.Web.Data;
using LeaveWise.Web.Models.LeaveTypes;

namespace LeaveWise.Web.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<LeaveType, LeaveTypeReadOnlyVM>()
            .ForMember(dest => dest.Days,
                opts => opts
                    .MapFrom(src => src.NumberOfDays));
    }
}