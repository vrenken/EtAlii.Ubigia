namespace EtAlii.xTechnology.Structure
{
    public interface IParams<out TParam>
    {
        TParam Parameter { get; }
    }


}
