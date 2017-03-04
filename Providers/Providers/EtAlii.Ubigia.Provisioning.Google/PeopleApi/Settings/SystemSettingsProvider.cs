namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{

    public class SystemSettingsProvider : ISystemSettingsProvider
    {
        public SystemSettings SystemSettings => _systemSettings;
        private SystemSettings _systemSettings;

        private readonly IProviderContext _context;
        private readonly ISystemSettingsGetter _getter;


        public SystemSettingsProvider(
            IProviderContext context,
            ISystemSettingsGetter getter)
        {
            _context = context;
            _getter = getter;
        }

        public void Update()
        {
            _systemSettings = _getter.Get(_context.SystemDataContext);
        }
    }
}