namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Diagnostics;

    [DebuggerStepThrough]
    [DebuggerDisplay("{Description}")]
    public class GraphCondition : GraphPathPart
    {
        public string Description { get; private set; }
        public Predicate<PropertyDictionary> Predicate { get; private set; }

        public GraphCondition(Predicate<PropertyDictionary> predicate, string description)
        {
            Predicate = predicate;
            Description = description;
        }

        public override string ToString()
        {
            return Description;
        }
    }
}