// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.xTechnology.MicroContainer;

    public class BlobsScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IBlobStorage, BlobStorage>();
            container.Register<IBlobStorer, BlobStorer>();
            container.Register<IBlobRetriever, BlobRetriever>();
            container.Register<IBlobPartStorer, BlobPartStorer>();
            container.Register<IBlobPartRetriever, BlobPartRetriever>();
            container.Register<IBlobSummaryCalculator, BlobSummaryCalculator>();
            container.RegisterDecorator(typeof(IBlobSummaryCalculator), typeof(LoadingBlobSummaryCalculatorDecorator));
        }
    }
}
