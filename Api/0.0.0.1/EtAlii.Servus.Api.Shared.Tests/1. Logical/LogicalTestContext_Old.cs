//namespace EtAlii.Servus.Api.Logical.Tests
//{
//    using System;
//    using System.Security.Policy;
//    using EtAlii.Servus.Api.Fabric;
//    using EtAlii.Servus.Api.Fabric.Tests;
//    using EtAlii.Servus.Infrastructure.Hosting;

//    public class LogicalTestContext : ILogicalTestContext
//    {
//        private readonly IFabricTestContext _fabric;

//        public LogicalTestContext(IFabricTestContext fabric)
//        {
//            _fabric = fabric;
//        }

//        public ILogicalContext CreateLogicalContext(bool openOnCreation = true, bool useNewSpace = false)
//        {
//            var fabric = _fabric.CreateFabricContext(openOnCreation, useNewSpace);
//            return new LogicalContextFactory().Create(fabric);
//        }

//        public string AddYearMonth(ILogicalContext context)
//        {
//            IEditableEntry monthEntry;
//            return AddYearMonth(context, out monthEntry);
//        }

//        public string AddYearMonth(ILogicalContext context, out IEditableEntry monthEntry)
//        {
//            var timeRoot = context.Roots.Get("Time");
//            var time = context.Nodes.Select(GraphPath.Create(timeRoot.Identifier));
//            var now = DateTime.Now;
//            string year = now.ToString("yyyy");
//            string month = now.ToString("MM");
//            monthEntry = CreateHierarchy(context, (IEditableEntry)time, year, month);
//            return String.Format("/Time/{0}/{1}", year, month);
//        }

//        public string AddYearMonthDayHourMinute(ILogicalContext logicalContext)
//        {
//            throw new NotImplementedException();
//        }

//        public void AddDays(ILogicalContext context, IEditableEntry monthEntry, int days)
//        {
//            for (int i = 1; i <= days; i++)
//            {
//                CreateHierarchy(context, monthEntry, String.Format("{0:00}", i));
//            }
//        }

//        public IEditableEntry CreateHierarchy(ILogicalContext context, IEditableEntry parent, params string[] hierarchy)
//        {
//            return _fabric.CreateHierarchy(context.Fabric, parent, hierarchy);
//        }

//        #region start/stop/reset

//        public void Start()
//        {
//            _fabric.Start();
//        }

//        public void Stop()
//        {
//            _fabric.Stop();
//        }

//        public void Reset()
//        {
//            _fabric.Reset();
//        }

//        #endregion start/stop/reset
//    }
//}