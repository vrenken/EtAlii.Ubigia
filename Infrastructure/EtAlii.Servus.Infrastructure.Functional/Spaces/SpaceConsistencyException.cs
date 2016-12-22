namespace EtAlii.Servus.Infrastructure.Functional
{
    using System;
    using EtAlii.Servus.Api;

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
