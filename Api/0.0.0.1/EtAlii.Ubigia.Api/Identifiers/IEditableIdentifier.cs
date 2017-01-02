namespace EtAlii.Ubigia.Api
{
    using System;

    public interface IEditableIdentifier
    {
        Guid Storage { get; set; }
        Guid Account { get; set; }
        Guid Space { get; set; }
        UInt64 Era { get; set; }
        UInt64 Period { get; set; }
        UInt64 Moment { get; set; }
    }
}
