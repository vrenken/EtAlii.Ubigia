namespace EtAlii.xTechnology.Hosting
{
    using System.Collections.Generic;

    public interface IServiceLogic
    {
        void Start(IEnumerable<string> args);
        void Stop();
    }

}
