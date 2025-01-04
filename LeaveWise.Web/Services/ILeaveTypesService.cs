using LeaveWise.Web.Models.LeaveTypes;

namespace LeaveWise.Web.Services;

public interface ILeaveTypesService
{
    Task<List<LeaveTypeReadOnlyVM>> GetAllAsync();

    Task<T?> GetAsync<T>(int id) where T : class;

    Task RemoveAsync(int id);

    Task CreateAsync(LeaveTypeCreateVM model);

    Task EditAsync(LeaveTypeEditVM model);

    Task<bool> LeaveTypeExistsAsync(int id);

    Task<bool> LeaveTypeNameExistsAsync(string name);

    Task<bool> LeaveTypeNameExistsForEditAsync(LeaveTypeEditVM leaveTypeEdit);
}