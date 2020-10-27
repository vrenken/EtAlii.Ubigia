namespace EtAlii.Ubigia
{
    using System;

    public interface IEditableIdentifier
    {
        Guid Storage { get; set; }
        Guid Account { get; set; }
        Guid Space { get; set; }
        ulong Era { get; set; }
        ulong Period { get; set; }
        ulong Moment { get; set; }
    }
}
