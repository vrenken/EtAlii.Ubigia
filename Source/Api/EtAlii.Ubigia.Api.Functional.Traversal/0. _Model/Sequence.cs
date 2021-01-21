namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;

    public class Sequence
    {
        public SequencePart[] Parts { get; }

        internal Sequence(SequencePart[] parts)
        {
            Parts = parts;
        }

        public override string ToString()
        {
            return string.Concat(Parts.Select(part => part.ToString()));
        }
    }
}
