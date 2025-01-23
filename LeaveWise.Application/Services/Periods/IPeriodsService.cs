using LeaveWise.Data;

namespace LeaveWise.Application.Services.Periods;

public interface IPeriodsService
{
    Task<Period> GetCurrentPeriodAsync();
}