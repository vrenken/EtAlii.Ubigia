﻿namespace EtAlii.Servus.Provisioning.Mail
{
    using EtAlii.Servus.Api.Functional;

    public class MailProvider : IProvider
    {
        public IProviderConfiguration Configuration { get { return _configuration; } }
        private readonly IProviderConfiguration _configuration;

        public MailProvider(IProviderConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Stop()
        {
        }

        public void Start()
        {
        }

    }
}
