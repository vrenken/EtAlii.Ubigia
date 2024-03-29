// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

public interface ITraversalValidator
{
    void Validate(Script script);
    void Validate(Subject subject);
}
