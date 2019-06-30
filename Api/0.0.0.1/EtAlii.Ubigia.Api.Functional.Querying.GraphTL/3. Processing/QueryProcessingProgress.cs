namespace EtAlii.Ubigia.Api.Functional
{
    public class QueryProcessingProgress
    {
        public Fragment Fragment { get; }
        public int Step { get; }
        public int Total { get; }

        public QueryProcessingProgress(Fragment fragment, int step, int total)
        {
            Fragment = fragment;
            Step = step;
            Total = total;
        }
    }
}