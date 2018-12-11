//namespace EtAlii.Ubigia.Api.Functional
//{
//    using EtAlii.xTechnology.MicroContainer;
//
//    public class DataContextFactory
//    {
//        public IDataContext Create(IDataContextConfiguration configuration)
//        {
//
//            var container = new Container();
//
//            var scaffoldings = new IScaffolding[]
//            {
//                new DataContextScaffolding(configuration),
//            };
//
//            foreach (var scaffolding in scaffoldings)
//            {
//                scaffolding.Register(container);
//            }
//
//            foreach (var extension in configuration.Extensions)
//            {
//                extension.Initialize(container);
//            }
//
//            return container.GetInstance<IDataContext>();
//        }
//    }
//}
