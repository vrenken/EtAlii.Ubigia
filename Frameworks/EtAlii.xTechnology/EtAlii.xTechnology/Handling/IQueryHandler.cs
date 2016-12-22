namespace EtAlii.xTechnology.Structure
{
    public interface IQueryHandler<TParam, out TResult>
    {
        IQuery<TParam> Create(TParam parameter); 
        TResult Handle(IQuery<TParam> query);
    }

}
