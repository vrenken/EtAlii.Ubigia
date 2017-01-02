namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Data.Model;
    using System;
    using System.Linq;

    public class Sequence
    {
        public IEnumerable<SequencePart> Parts { get; private set; }

        internal Sequence(IEnumerable<SequencePart> parts)
        {
            this.Parts = parts;
        }

        public override string ToString()
        {
            return String.Concat(Parts.Select(part => part.ToString()));
        }
    }
}
