//namespace EtAlii.Ubigia.Infrastructure.Hosting
//{
//    using System.Collections.Generic
//    using System.Configuration
//    using System.Linq
//    using EtAlii.xTechnology.Hosting

//    public class ServicesConfigurationSectionGroup : ConfigurationSectionGroup, IServicesConfigurationSectionGroup
//    {
//        public IHostService[] ToHostServices()
//        {
//            var result = new List<IHostService>()

//            foreach (var serviceConfigurationSection in this.Sections.OfType<IServicesConfigurationSection>())
//            {
//                var hostService = serviceConfigurationSection.ToService()
//                result.Add(hostService)
//            }
//            return result.ToArray()
//        }
//    }
//}