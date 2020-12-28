﻿namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    public class SchemaParserTestContainerFactory
    {
        public Container Create()
        {
            var scaffoldings = new IScaffolding[]
            {
                new SchemaParserScaffolding(),
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