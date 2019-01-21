namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using EtAlii.Ubigia.Api;

    [Serializable]
    public class SpaceConsistencyException : Exception
    {
        public SpaceConsistencyException(Identifier source, Identifier target, string message)
            : base(message)
        {
            Data["source"] = source;
            Data["target"] = target;
        }
    }
}
