using System;

namespace EtAlii.Servus.Api
{
    public class Account : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}