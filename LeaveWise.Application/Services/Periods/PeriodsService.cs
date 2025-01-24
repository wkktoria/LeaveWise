namespace LeaveWise.Application.Services.Periods;

public class PeriodsService(ApplicationDbContext context) : IPeriodsService
{
    public async Task<Period> GetCurrentPeriodAsync()
    {
        var currentDate = DateTime.Now;
        var period = await context.Periods.SingleAsync(p => p.EndDate.Year == currentDate.Year);
        return period;
    }
}