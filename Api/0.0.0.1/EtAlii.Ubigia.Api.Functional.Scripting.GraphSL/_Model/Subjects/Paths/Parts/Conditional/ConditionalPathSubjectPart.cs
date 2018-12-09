namespace EtAlii.Ubigia.Api.Functional
{
    using System;
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
            return String.Join("&", Conditions.Select(c => c.ToString()));
        }
    }
}
