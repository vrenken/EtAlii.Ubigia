namespace EtAlii.Ubigia.Api.Functional
{
    public class ScriptProcessingProgress
    {
        public Sequence Sequence { get; private set; }
        public int Step { get; private set; }
        public int Total { get; private set; }

        public ScriptProcessingProgress(Sequence sequence, int step, int total)
        {
            Sequence = sequence;
            Step = step;
            Total = total;
        }
    }
}