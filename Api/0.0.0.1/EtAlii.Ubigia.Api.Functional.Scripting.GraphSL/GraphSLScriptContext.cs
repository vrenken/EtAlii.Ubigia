namespace EtAlii.Ubigia.Api.Functional.Scripting.GraphSL
{
    using EtAlii.Ubigia.Api.Functional.Scripting.GraphSL;
    using System.Threading.Tasks;

    internal class GraphSLScriptContext : IGraphSLScriptContext
    {
        private readonly IDataContext _dataContext;


        internal GraphSLScriptContext(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
    }
}