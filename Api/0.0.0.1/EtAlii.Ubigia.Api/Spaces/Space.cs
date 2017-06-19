namespace EtAlii.Ubigia.Api
{
    using System;

    /// <summary>
    /// Represents a single space in a <see cref="Storage"/> .
    /// </summary>
    public class Space : IIdentifiable
    {
        /// <summary>
        /// The unique identifier of the Space.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the Space.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The account to which this Space belongs.
        /// </summary>
        public Guid AccountId { get; set; }
    }
}