// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;

    public class FunctionHandlersProvider : IFunctionHandlersProvider
    {
        public IFunctionHandler[] FunctionHandlers { get; }

        /// <summary>
        /// An empty FunctionHandlersProvider.
        /// </summary>
        public static IFunctionHandlersProvider Empty { get; } = new FunctionHandlersProvider(Array.Empty<IFunctionHandler>());

        public FunctionHandlersProvider(IFunctionHandler[] functionHandlers)
        {
            FunctionHandlers = functionHandlers ?? throw new ArgumentException("No function handlers specified", nameof(functionHandlers));
        }

        public FunctionHandlersProvider(IFunctionHandler[] functionHandlers1, IFunctionHandler[] functionHandlers2)
        {
            if (functionHandlers1 == null)
            {
                throw new ArgumentException("No first set of function handlers specified", nameof(functionHandlers1));
            }
            if (functionHandlers2 == null)
            {
                throw new ArgumentException("No second set of function handlers specified", nameof(functionHandlers2));
            }
            FunctionHandlers = functionHandlers1
                .Where(fh1 => functionHandlers2.All(fh2 => fh2.GetType() != fh1.GetType()))
                .Concat(functionHandlers2)
                .Distinct()
                .ToArray();
        }
    }
}
