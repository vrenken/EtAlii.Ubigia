namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Threading.Tasks;

    public class SystemSettingsProvider : ISystemSettingsProvider
    {
        public SystemSettings SystemSettings { get; private set; }

        private readonly IProviderContext _context;
        private readonly ISystemSettingsGetter _getter;


        public SystemSettingsProvider(
            IProviderContext context,
            ISystemSettingsGetter getter)
        {
            _context = context;
            _getter = getter;
        }

        public async Task Update()
        {
            SystemSettings = await _getter.Get(_context.SystemScriptContext);
        }
    }
}