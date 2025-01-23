using LeaveWise.Application.Models.LeaveTypes;

namespace LeaveWise.Application.Services.LeaveTypes;

public interface ILeaveTypesService
{
    Task<List<LeaveTypeReadOnlyVm>> GetAllAsync();

    Task<T?> GetAsync<T>(int id) where T : class;

    Task RemoveAsync(int id);

    Task CreateAsync(LeaveTypeCreateVm model);

    Task EditAsync(LeaveTypeEditVm model);

    Task<bool> LeaveTypeExistsAsync(int id);

    Task<bool> LeaveTypeNameExistsAsync(string name);

    Task<bool> LeaveTypeNameExistsForEditAsync(LeaveTypeEditVm leaveTypeEdit);
    
    Task<bool> DaysExceedMaximumAsync(int leaveTypeId, int days);
}