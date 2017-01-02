//namespace EtAlii.Servus.Provisioning.Google.PeopleApi
//{
//    using System.Threading.Tasks;
//    using EtAlii.Servus.Api;

//    public class PeopleApiSpaceChecker : IPeopleApiSpaceChecker
//    {
//        private readonly IProviderContext _context;

//        public PeopleApiSpaceChecker(IProviderContext context)
//        {
//            _context = context;
//        }

//        public bool HasPeopleApiConfigurations(ConfigurationSpace configuration)
//        {
//            var result = false;
//            var task = Task.Run(async () =>
//            {
//                var dataConnection = await _context.ManagementConnection.OpenSpace(configuration.Space);
//                var context = dataConnection.CreateDataContext();
//                context.Scripts.Process("/")

//            });
//            return result;
//        }
//    }
//}