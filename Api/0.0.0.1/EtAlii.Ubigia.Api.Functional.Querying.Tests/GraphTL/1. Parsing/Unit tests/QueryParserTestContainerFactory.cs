namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using EtAlii.xTechnology.MicroContainer;

    public class QueryParserTestContainerFactory 
    {
        public Container Create()
        {
            var scaffoldings = new IScaffolding[]
            {
                new QueryParserScaffolding(),
                new SequenceParsingScaffolding(), 
                new SubjectParsingScaffolding(), 
                new PathSubjectParsingScaffolding(),
                new OperatorParsingScaffolding(), 
                new ConstantHelpersScaffolding(), 
            };
            
                        
            var container = new Container();

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            return container;
        }
    }
}
