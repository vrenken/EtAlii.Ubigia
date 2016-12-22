﻿    namespace EtAlii.Servus.Api.Functional
{
    public class ProcessParameters<TTarget, TPart>
    {
        public TPart FuturePart { get; set; }
        public TPart RightPart { get; set; }
        public TPart LeftPart { get; set; }
        public object RightResult { get; set; }
        public object LeftResult { get; set; }

        public TTarget Target { get; private set;}

        public ProcessParameters(TTarget target)
        {
            Target = target;
        }
    }
}
