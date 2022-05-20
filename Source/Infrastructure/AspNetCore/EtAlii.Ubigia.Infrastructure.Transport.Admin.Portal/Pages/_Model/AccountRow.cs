namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal
{
    using System;

    public record AccountRow
    {
        public int Index;
        public string Name;
        public Guid Id;
        public int Spaces;
    }
}
