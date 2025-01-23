using AutoMapper;
using LeaveWise.Application.Models.LeaveRequests;
using LeaveWise.Data;

namespace LeaveWise.Application.MappingProfiles;

public class LeaveRequestAutoMapperProfile : Profile
{
    public LeaveRequestAutoMapperProfile()
    {
        CreateMap<LeaveRequestCreateVm, LeaveRequest>();
    }
}