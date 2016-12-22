namespace EtAlii.Servus.Api.Logical.Tests
{
    using System.Threading.Tasks;

    public interface ILogicalTestContext
    {
        Task<ILogicalContext> CreateLogicalContext(bool openOnCreation);
        Task AddDays(ILogicalContext context, IEditableEntry monthEntry, int days);
        Task<IEditableEntry> CreateHierarchy(ILogicalContext context, IEditableEntry parent, params string[] hierarchy);
        Task<TimeAddResult> AddYearMonth(ILogicalContext context);
        Task AddTime(ILogicalContext context, int year, int month, int day, int hour, int minute, int second);
        Task AddTime(ILogicalContext context, int year, int month, int day, int hour, int minute);
        Task AddTime(ILogicalContext context, int year, int month, int day, int hour);
        Task AddTime(ILogicalContext context, int year, int month, int day);
        Task AddTime(ILogicalContext context, int year, int month);
        Task AddTime(ILogicalContext context, int year);

        Task<string> AddYearMonthDayHourMinute(ILogicalContext logicalContext);

        Task Start();
        Task Stop();
    }
}