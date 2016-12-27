﻿namespace EtAlii.Servus.Api.Functional
{
    internal interface IRootHandlerFinder
    {
        IRootHandler Find(IScriptScope scope, IRootHandlerMapper rootHandlerMapper, RootedPathSubject rootedPathSubject);
    }
}