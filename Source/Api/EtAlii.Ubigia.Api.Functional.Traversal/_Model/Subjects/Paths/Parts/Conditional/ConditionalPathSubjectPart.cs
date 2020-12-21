namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;

    public class ConditionalPathSubjectPart : PathSubjectPart
    {
        public Condition[] Conditions { get; }

        public ConditionalPathSubjectPart(Condition[] conditions)
        {
            Conditions = conditions;
        }

        public override string ToString()
        {
            return string.Join("&", Conditions.Select(c => c.ToString()));
        }
    }
}
