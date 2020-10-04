namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.xTechnology.MicroContainer;

    public class BlobsScaffolding : IScaffolding
    {
        public void Register(Container container)
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
