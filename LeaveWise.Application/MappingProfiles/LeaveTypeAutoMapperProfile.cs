using AutoMapper;
using LeaveWise.Application.Models.LeaveTypes;
using LeaveWise.Data;

namespace LeaveWise.Application.MappingProfiles;

public class LeaveTypeAutoMapperProfile : Profile
{
    public LeaveTypeAutoMapperProfile()
    {
        CreateMap<LeaveType, LeaveTypeReadOnlyVm>()
            .ForMember(dest => dest.NumberOfDays,
                opts => opts
                    .MapFrom(src => src.NumberOfDays));
        CreateMap<LeaveTypeCreateVm, LeaveType>();
        CreateMap<LeaveTypeEditVm, LeaveType>().ReverseMap();
    }
}