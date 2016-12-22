//namespace EtAlii.Servus.Provisioning.Hosting
//{
//    using EtAlii.Servus.Provisioning.Mail;
//    using EtAlii.Servus.Provisioning.Time;
//    using EtAlii.Servus.Provisioning.Twitter;
//    using EtAlii.xTechnology.MicroContainer;

//    public class ProviderComponentsScaffolding : IScaffolding
//    {
//        public void Register(Container container)
//        {
//            container.Register<TimeImporter, TimeImporter>();
//            container.Register<tweetImporter, tweetImporter>();
//            container.Register<MailImporter, MailImporter>();
//            container.Register<IProviderComponent[]>(() =>
//            {
//                return new IProviderComponent[]
//                {
//                    container.GetInstance<TimeImporter>(),
//                    container.GetInstance<tweetImporter>(),
//                    container.GetInstance<MailImporter>(),
//                };
//            });
//        }
//    }
//}
