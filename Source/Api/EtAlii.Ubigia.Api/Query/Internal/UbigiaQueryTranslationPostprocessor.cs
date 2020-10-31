// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace EtAlii.Ubigia.Api.Query.Internal
{
    public class UbigiaQueryTranslationPostprocessor : QueryTranslationPostprocessor
    {
        public UbigiaQueryTranslationPostprocessor(QueryTranslationPostprocessorDependencies dependencies)
            : base(dependencies)
        {
        }

        public override Expression Process(Expression query)
        {
            query = base.Process(query);

            return query;
        }
    }
}
