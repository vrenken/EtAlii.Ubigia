namespace EtAlii.Servus.Api.Logical.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Fabric.Tests;

    public class LogicalTestContext2 : ILogicalTestContext
    {
        private readonly IFabricTestContext _fabric;

        public LogicalTestContext2(IFabricTestContext fabric)
        {
            _fabric = fabric;
        }

        public async Task<ILogicalContext> CreateLogicalContext(bool openOnCreation)
        {
            var fabric = await _fabric.CreateFabricContext(openOnCreation);
            var configuration = new LogicalContextConfiguration()
                .Use(fabric);
            return new LogicalContextFactory().Create(configuration);
        }

        public async Task<TimeAddResult> AddYearMonth(ILogicalContext context)
        {
            var scope = new ExecutionScope(false);
            // Root.
            // Time.
            // [LINK]
            // yyyy
            // [LINK]
            // mm

            var timeRoot = await context.Roots.Get("Time");
            var now = DateTime.Now;
            string year = now.ToString("yyyy");
            string month = now.ToString("MM");

            var yearEntry = await context.Nodes.Add(timeRoot.Identifier, year, scope);
            var monthEntry = (IEditableEntry)await context.Nodes.Add(yearEntry.Id, month, scope);
            var path = String.Format("/Time/{0}/{1}", year, month);
            return new TimeAddResult(path, monthEntry);
        }

        public async Task<string> AddYearMonthDayHourMinute(ILogicalContext context)
        {
            var timeRoot = await context.Roots.Get("Time");
            var now = DateTime.Now;
            string year = now.ToString("yyyy");
            string month = now.ToString("MM");
            string day = now.ToString("dd");
            string hour = now.ToString("HH");
            string minute = now.ToString("mm");

            var scope = new ExecutionScope(false);

            var yearEntry = await context.Nodes.Add(timeRoot.Identifier, year, scope);
            var monthEntry = (IEditableEntry)await context.Nodes.Add(yearEntry.Id, month, scope);
            var dayEntry = (IEditableEntry)await context.Nodes.Add(monthEntry.Id, day, scope);
            var hourEntry = (IEditableEntry)await context.Nodes.Add(dayEntry.Id, hour, scope);
            var minuteEntry = (IEditableEntry)await context.Nodes.Add(hourEntry.Id, minute, scope);
            return String.Format("/Time/{0}/{1}/{2}/{3}/{4}", year, month, day, hour, minute);
        }

        public async Task AddDays(ILogicalContext context, IEditableEntry monthEntry, int days)
        {
            for (int i = 1; i <= days; i++)
            {
                await CreateHierarchy(context, monthEntry, String.Format("{0:00}", i));
            }
        }

        public async Task<IEditableEntry> CreateHierarchy(ILogicalContext context, IEditableEntry parent, params string[] hierarchy)
        {
            var scope = new ExecutionScope(false);

            IEditableEntry result = parent;
            foreach (var element in hierarchy)
            {
                result = (IEditableEntry)await context.Nodes.Add(result.Id, element, scope);
            }
            return result;
        }


        #region start/stop

        public async Task Start()
        {
            await _fabric.Start();
        }

        public async Task Stop()
        {
            await _fabric.Stop();
        }

        #endregion start/stop
    }
}