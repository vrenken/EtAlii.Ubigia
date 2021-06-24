// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

    namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public class ProcessParameters<TTarget, TPart>
    {
        public TPart FuturePart { get; set; }
        public TPart RightPart { get; set; }
        public TPart LeftPart { get; set; }
        public object RightResult { get; set; }
        public object LeftResult { get; set; }

        public TTarget Target { get; }

        public ProcessParameters(TTarget target)
        {
            Target = target;
        }
    }
}
