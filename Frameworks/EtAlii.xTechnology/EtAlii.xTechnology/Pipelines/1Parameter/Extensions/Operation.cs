namespace EtAlii.xTechnology.Structure.Pipelines2
{
    public delegate TOut Operation<in TIn, out TOut>(TIn input);

    public delegate void Operation<in TValue>(TValue input);
}