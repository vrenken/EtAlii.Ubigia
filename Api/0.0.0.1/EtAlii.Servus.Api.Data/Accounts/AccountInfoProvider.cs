namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Client.Model;
    using System;

    public class AccountInfoProvider: IAccountInfoProvider 
    {
        public string AccountName { get; set; }
        public Account Account { get; set; }
        public Guid AccountId { get; set; }

        public Storage TargetStorage { get; set; }
    }
}
