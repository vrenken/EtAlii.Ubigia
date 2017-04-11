namespace EtAlii.Ubigia.Api.Functional
{
    public class ScriptProcessingProgress
    {
        public Sequence Sequence { get; }
        public int Step { get; }
        public int Total { get; }

        public ScriptProcessingProgress(Sequence sequence, int step, int total)
        {
            Sequence = sequence;
            Step = step;
            Total = total;
        }
    }
}