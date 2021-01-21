namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The Script class contains all information needed to execute actions on the current infrastructureClient.
    /// </summary>
    public class Script
    {
        /// <summary>
        /// The executable sequences that make up the script.
        /// </summary>
        public IEnumerable<Sequence> Sequences { get; }

        internal Script(Sequence sequences) => Sequences = new[] { sequences };

        internal Script(IEnumerable<Sequence> sequences) => Sequences = sequences;

        public override string ToString() => string.Join(Environment.NewLine, Sequences);
    }
}
