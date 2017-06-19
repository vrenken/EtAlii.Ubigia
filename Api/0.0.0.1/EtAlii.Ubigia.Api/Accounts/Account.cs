namespace EtAlii.Ubigia.Api
{
    using System;

    public class Account : IIdentifiable
    {
        public Account()
        {
            Roles = new string[0];
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}