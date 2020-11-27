namespace EtAlii.xTechnology.MicroContainer
{
    /// <summary>
    /// A generic factory definition useful when building complex DI structures. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFactory<out T>
	{
        /// <summary>
        /// Create a new instance using the internal container registrations. 
        /// </summary>
        /// <returns></returns>
        T Create();
    }
}
