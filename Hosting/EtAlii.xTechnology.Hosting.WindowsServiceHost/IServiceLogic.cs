namespace EtAlii.xTechnology.Hosting
{
    using System.Collections.Generic;

    public interface IServiceLogic
    {
        string Name { get; }
        string DisplayName { get; }
        string Description { get; }

        void Start(IEnumerable<string> args);
        void Stop();
    }

}
