namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    public class Sequence
    {
        public SequencePart[] Parts { get; private set; }

        internal Sequence(SequencePart[] parts)
        {
            this.Parts = parts;
        }

        public override string ToString()
        {
            return String.Concat(Parts.Select(part => part.ToString()));
        }
    }
}
