// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Threading.Tasks;

    public static class FunctionalTestContextUseLogicalContextExtension
    {
        public static FunctionalOptions UseLogicalContext(this FunctionalOptions options, FunctionalUnitTestContext context)
        {
            var logicalContext = context.Logical
                .CreateLogicalContextWithoutConnection();

            return options.UseLogicalContext(logicalContext);
        }

        public static async Task<FunctionalOptions> UseLogicalContext(this FunctionalOptions options, FunctionalUnitTestContext context, bool openOnCreation)
        {
            var logicalContext = await context.Logical
                .CreateLogicalContextWithConnection(openOnCreation)
                .ConfigureAwait(false);

            return options.UseLogicalContext(logicalContext);
        }
    }
}
