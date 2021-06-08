namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    public class LapaSchemaParserTestContainerFactory
    {
        public Container Create()
        {
            var scaffoldings = new IScaffolding[]
            {
                new LapaSchemaParserScaffolding(),

                new LapaSequenceParsingScaffolding(),
                new LapaSubjectParsingScaffolding(),
                new LapaOperatorParsingScaffolding(),
                new LapaPathSubjectParsingScaffolding(),
                new LapaConstantParsingScaffolding(),
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
