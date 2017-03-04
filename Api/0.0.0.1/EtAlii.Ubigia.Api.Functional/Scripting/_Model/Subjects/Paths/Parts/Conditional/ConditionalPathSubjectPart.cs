namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    public class ConditionalPathSubjectPart : PathSubjectPart
    {
        public Condition[] Conditions => _conditions;
        private readonly Condition[] _conditions; 

        public ConditionalPathSubjectPart(Condition[] conditions)
        {
            _conditions = conditions;
        }

        public override string ToString()
        {
            return String.Join("&", _conditions.Select(c => c.ToString()));
        }
    }
}
