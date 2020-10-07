namespace EtAlii.xTechnology.MicroContainer
{
    public interface IFactory<out T>
	{
        T Create();
    }
}
