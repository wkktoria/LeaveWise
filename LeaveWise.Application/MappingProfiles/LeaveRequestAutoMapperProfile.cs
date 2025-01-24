using LeaveWise.Application.Models.LeaveRequests;

namespace LeaveWise.Application.MappingProfiles;

public class LeaveRequestAutoMapperProfile : Profile
{
    public LeaveRequestAutoMapperProfile()
    {
        CreateMap<LeaveRequestCreateVm, LeaveRequest>();
    }
}