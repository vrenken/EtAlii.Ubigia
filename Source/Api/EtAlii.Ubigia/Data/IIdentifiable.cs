namespace EtAlii.Ubigia
{
    using System;

    /// <summary>
    /// This interface represents identifiable, named types.
    /// </summary>
    public interface IIdentifiable
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}