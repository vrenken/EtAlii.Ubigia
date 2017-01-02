namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;

    public class PathFunctionSubjectArgument : FunctionSubjectArgument
    {
        public PathSubjectPart[] Parts { get; private set; }
        public bool IsAbsolute { get { return Parts.FirstOrDefault() is IsParentOfPathSubjectPart; } }

        public PathFunctionSubjectArgument(PathSubjectPart[] parts)
        {
            this.Parts = parts;
        }

        public override string ToString()
        {
            return String.Concat(Parts.Select(part => part.ToString()));
        }
    }
}
