using LeaveWise.Application.Models.LeaveAllocations;
using LeaveWise.Application.Models.Periods;

namespace LeaveWise.Application.MappingProfiles;

public class LeaveAllocationAutoMapperProfile : Profile
{
    public LeaveAllocationAutoMapperProfile()
    {
        CreateMap<LeaveAllocation, LeaveAllocationVm>();
        CreateMap<LeaveAllocation, LeaveAllocationEditVm>();
        CreateMap<ApplicationUser, EmployeeListVm>();
        CreateMap<Period, PeriodVM>();
    }
}