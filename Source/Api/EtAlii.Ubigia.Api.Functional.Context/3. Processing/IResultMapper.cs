namespace EtAlii.Ubigia.Api.Functional.Context
{
    public interface IResultMapper<out TResult>
    {
        TResult MapRoot(Structure structure);
    }
}
