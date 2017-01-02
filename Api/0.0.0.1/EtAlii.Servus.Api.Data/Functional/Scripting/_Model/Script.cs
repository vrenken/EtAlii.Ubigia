namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// The Script class contains all information needed to execute actions on the current infrastructureClient.
    /// </summary>
    public class Script
    {
        public IEnumerable<Sequence> Sequences { get; private set; }

        internal Script(Sequence sequences)
        {
            this.Sequences = new Sequence[] { sequences };
        }

        internal Script(IEnumerable<Sequence> sequences)
        {
            this.Sequences = sequences;
        }
    }
}
