﻿namespace EtAlii.Ubigia.Api.Functional
{
    /// <summary>
    /// This PoCo represents a path subject without an absolute point of reference.
    /// It can only be used in conjunction with other path subjects or starting Identifiers.  
    /// </summary>
    public class RelativePathSubject : NonRootedPathSubject
    {
        public RelativePathSubject(PathSubjectPart part)
            : base(part)
        {
        }

        public RelativePathSubject(PathSubjectPart[] parts)
            : base(parts)
        {
        }
    }
}
