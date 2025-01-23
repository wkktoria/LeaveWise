using AutoMapper;
using LeaveWise.Web.Models.LeaveRequests;

namespace LeaveWise.Web.MappingProfiles;

public class LeaveRequestAutoMapperProfile : Profile
{
    public LeaveRequestAutoMapperProfile()
    {
        CreateMap<LeaveRequestCreateVM, LeaveRequest>();
    }
}