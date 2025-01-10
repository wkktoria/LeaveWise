using AutoMapper;
using LeaveWise.Web.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace LeaveWise.Web.Services.LeaveTypes;

public class LeaveTypesService(ApplicationDbContext context, IMapper mapper) : ILeaveTypesService
{
    public async Task<List<LeaveTypeReadOnlyVM>> GetAllAsync()
    {
        var data = await context.LeaveTypes.ToListAsync();
        return mapper.Map<List<LeaveTypeReadOnlyVM>>(data);
    }

    public async Task<T?> GetAsync<T>(int id) where T : class
    {
        var data = await context.LeaveTypes.FirstOrDefaultAsync(l => l.Id == id);
        return data == null ? null : mapper.Map<T>(data);
    }

    public async Task RemoveAsync(int id)
    {
        var data = await context.LeaveTypes.FirstOrDefaultAsync(l => l.Id == id);

        if (data == null)
        {
            return;
        }

        context.Remove(data);
        await context.SaveChangesAsync();
    }

    public async Task CreateAsync(LeaveTypeCreateVM model)
    {
        var leaveType = mapper.Map<LeaveType>(model);
        await context.LeaveTypes.AddAsync(leaveType);
        await context.SaveChangesAsync();
    }

    public async Task EditAsync(LeaveTypeEditVM model)
    {
        var leaveType = mapper.Map<LeaveType>(model);
        context.LeaveTypes.Update(leaveType);
        await context.SaveChangesAsync();
    }

    public async Task<bool> LeaveTypeExistsAsync(int id)
    {
        return await context.LeaveTypes.AnyAsync(e => e.Id == id);
    }

    public async Task<bool> LeaveTypeNameExistsAsync(string name)
    {
        var lowercaseName = name.ToLower();
        return await context.LeaveTypes.AnyAsync(l => l.Name.ToLower().Equals(lowercaseName));
    }

    public async Task<bool> LeaveTypeNameExistsForEditAsync(LeaveTypeEditVM leaveTypeEdit)
    {
        var lowercaseName = leaveTypeEdit.Name.ToLower();
        return await context.LeaveTypes.AnyAsync(l =>
            l.Name.ToLower().Equals(lowercaseName) && l.Id != leaveTypeEdit.Id);
    }

    public async Task<bool> DaysExceedMaximumAsync(int leaveTypeId, int days)
    {
        var leaveType = await context.LeaveTypes.FindAsync(leaveTypeId);
        return leaveType?.NumberOfDays <= days;
    }
}