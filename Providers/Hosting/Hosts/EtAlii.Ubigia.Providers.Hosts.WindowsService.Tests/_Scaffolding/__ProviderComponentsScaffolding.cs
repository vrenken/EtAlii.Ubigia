//namespace EtAlii.Ubigia.Provisioning.Hosting
//{
//    using EtAlii.Ubigia.Provisioning.Mail;
//    using EtAlii.Ubigia.Provisioning.Time;
//    using EtAlii.Ubigia.Provisioning.Twitter;
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
