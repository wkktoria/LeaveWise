namespace LeaveWise.Web.Services.Periods;

public interface IPeriodsService
{
    Task<Period> GetCurrentPeriodAsync();
}