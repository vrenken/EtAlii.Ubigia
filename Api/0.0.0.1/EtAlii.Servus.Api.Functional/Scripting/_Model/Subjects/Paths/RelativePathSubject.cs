﻿namespace EtAlii.Servus.Api.Functional
{
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
