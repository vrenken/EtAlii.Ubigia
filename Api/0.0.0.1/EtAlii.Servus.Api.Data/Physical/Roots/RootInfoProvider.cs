
namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Client.Model;
    using System;

    public class RootInfoProvider : IRootInfoProvider, ISpaceInfoProvider
    {
        public string RootName { get; set; }
        public Root Root { get; set; }
        public Guid RootId { get; set; }

        public Storage TargetStorage { get; set; }

        public string SpaceName { get; set; }
        public Space Space { get; set; }
        public Guid SpaceId { get; set; }
        public Account Account { get; set; }
        public string AccountName { get; set; }
        public Guid AccountId { get; set; }
    }
}
