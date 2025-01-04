using AutoMapper;
using LeaveWise.Web.Data;
using LeaveWise.Web.Models.LeaveTypes;

namespace LeaveWise.Web.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<LeaveType, LeaveTypeReadOnlyVM>()
            .ForMember(dest => dest.NumberOfDays,
                opts => opts
                    .MapFrom(src => src.NumberOfDays));
        CreateMap<LeaveTypeCreateVM, LeaveType>();
        CreateMap<LeaveTypeEditVM, LeaveType>().ReverseMap();
    }
}