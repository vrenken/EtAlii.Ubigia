namespace EtAlii.Ubigia.Api.Functional
{
    public class TraversingWildcardPathSubjectPart : PathSubjectPart
    {
        public int Limit { get; private set; }

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
