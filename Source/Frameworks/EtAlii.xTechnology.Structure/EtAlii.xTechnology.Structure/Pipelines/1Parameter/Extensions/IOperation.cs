namespace EtAlii.xTechnology.Structure.Pipelines
{
    public interface IOperation<in TIn, out TOut>
    {
        TOut Process(TIn input);
    }

    public interface IOperation<in TIn>
    {
        void Process(TIn input);
    }
}