// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google
{
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Management;
    using EtAlii.Ubigia.Provisioning.Google.PeopleApi;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class GoogleProviderFactory : IProviderFactory
    {
        public IProvider Create(IProviderConfiguration configuration)
        {
            var container = new Container();

            container.Register<IProviderConfiguration>(() => configuration);
            container.Register<IDataContext>(() => configuration.SystemDataContext);
            container.Register<IManagementConnection>(() => configuration.ManagementConnection);
            container.Register<IProviderContext, ProviderContext>();
            container.Register<IProvider, GoogleProvider>();

            container.Register<ISystemSettingsProvider, SystemSettingsProvider>();
            container.Register<ISystemSettingsGetter, SystemSettingsGetter>();
            container.Register<ISystemSettingsSetter, SystemSettingsSetter>();

            container.Register<IUserSettingsSetter, UserSettingsSetter>();
            container.Register<IUserSettingsGetter, UserSettingsGetter>();
            //container.Register<IDataContext>(() => configuration.);

            container.Register<IMailImporter, MailImporter>();
            container.Register<ICalendarImporter, CalendarImporter>();

            container.Register<IPeopleImporter, PeopleImporter>();
            container.Register<IPeopleDataSpaceUpdater, PeopleDataSpaceUpdater>();
            container.Register<IPersonSetter, PersonSetter>();

            container.Register<IPeopleApiUpdater, PeopleApiUpdater>();
            container.Register<IPeopleApiConfigurationSpaceUpdater, PeopleApiConfigurationSpaceUpdater>();
            container.Register<IUserSettingsUpdater, UserSettingsUpdater>();

            container.Register<IConfigurationSpaceGetter, ConfigurationSpaceGetter>();

            container.Register<ILogger>(() => configuration.LogFactory.Create("Google.PeopleApi", "Provider"));

            container.RegisterDecorator(typeof(IPeopleApiConfigurationSpaceUpdater), typeof(DebuggingPeopleApiConfigurationSpaceUpdater));
            container.RegisterDecorator(typeof(IUserSettingsUpdater), typeof(DebuggingUserSettingsUpdater));
            container.RegisterDecorator(typeof(IPeopleApiUpdater), typeof(DebuggingPeopleApiUpdater));

            container.RegisterDecorator(typeof(IPersonSetter), typeof(DebuggingPersonSetter));
            container.RegisterDecorator(typeof(IPeopleImporter), typeof(DebuggingPeopleImporter));

            container.RegisterDecorator(typeof(IUserSettingsGetter), typeof(DebuggingUserSettingsGetter));
            container.RegisterDecorator(typeof(IUserSettingsSetter), typeof(DebuggingUserSettingsSetter));
            container.RegisterDecorator(typeof(ISystemSettingsSetter), typeof(DebuggingSystemSettingsSetter));
            container.RegisterDecorator(typeof(ISystemSettingsGetter), typeof(DebuggingSystemSettingsGetter));


            return container.GetInstance<IProvider>();
        }
    }
}
