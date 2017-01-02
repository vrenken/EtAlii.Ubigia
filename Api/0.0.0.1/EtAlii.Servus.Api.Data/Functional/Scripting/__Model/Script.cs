namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// The Script class contains all information needed to execute actions on the current infrastructureClient.
    /// </summary>
    public class Script
    {
        public IEnumerable<Action> Actions { get; private set; }

        internal Script(IEnumerable<Action> actions)
        {
            this.Actions = actions;
        }
    }
}
