using AutoMapper;
using LeaveWise.Web.Models.LeaveAllocations;
using LeaveWise.Web.Models.Periods;

namespace LeaveWise.Web.MappingProfiles;

public class LeaveAllocationAutoMapperProfile : Profile
{
    public LeaveAllocationAutoMapperProfile()
    {
        CreateMap<LeaveAllocation, LeaveAllocationVM>();
        CreateMap<LeaveAllocation, LeaveAllocationEditVM>();
        CreateMap<ApplicationUser, EmployeeListVM>();
        CreateMap<Period, PeriodVM>();
    }
}