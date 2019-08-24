namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    public class TraversingWildcardPathSubjectPart : PathSubjectPart
    {
        public int Limit { get; }

        public TraversingWildcardPathSubjectPart(int limit)
        {
            Limit = limit;
        }

        public override string ToString()
        {
            return Limit == 0 ? "**" : $"*{Limit}*";
        }
    }
}
