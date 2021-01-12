// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Context.Antlr;

    public partial class ContextVisitor
    {
        public override object VisitRequirement(ContextSchemaParser.RequirementContext context)
        {
            if(context.QUESTION() != null)
            {
                return Requirement.Optional;
            }
            if (context.EXCLAMATION() != null)
            {
                return Requirement.Mandatory;
            }

            return Requirement.None;
        }
    }
}
