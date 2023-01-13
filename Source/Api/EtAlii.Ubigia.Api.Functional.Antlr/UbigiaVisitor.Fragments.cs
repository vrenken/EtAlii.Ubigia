// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr;

using EtAlii.Ubigia.Api.Functional.Context;

public partial class UbigiaVisitor
{
    public override object VisitRequirement(UbigiaParser.RequirementContext context)
    {
        if (context != null)
        {
            if (context.QUESTION() != null)
            {
                return Requirement.Optional;
            }

            if (context.EXCLAMATION() != null)
            {
                return Requirement.Mandatory;
            }
        }

        return Requirement.None;
    }
}
