namespace EtAlii.Ubigia.Api.Functional.Scripting.GraphSL
{
    using EtAlii.xTechnology.MicroContainer;

    public class GraphSLScriptContextFactory
    {
        public IGraphSLScriptContext Create(IDataContext dataContext)
        {
            var container = new Container();
            
            var scaffoldings = new IScaffolding[]
            {
                //new GraphSLScriptContextScaffolding(dataContext), 
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
            
            return container.GetInstance<IGraphSLScriptContext>();
        }
    }
}