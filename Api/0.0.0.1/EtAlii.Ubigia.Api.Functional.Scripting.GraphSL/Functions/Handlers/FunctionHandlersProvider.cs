namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using System.Linq;

    public class FunctionHandlersProvider : IFunctionHandlersProvider
    {
        public IFunctionHandler[] FunctionHandlers { get; }

        /// <summary>
        /// An empty FunctionHandlersProvider.
        /// </summary>
        public static IFunctionHandlersProvider Empty { get; } = new FunctionHandlersProvider(new IFunctionHandler[] {});

        public FunctionHandlersProvider(IFunctionHandler[] functionHandlers)
        {
            if (functionHandlers == null)
            {
                throw new ArgumentException(nameof(functionHandlers));
            }
            FunctionHandlers = functionHandlers;
        }

        public FunctionHandlersProvider(IFunctionHandler[] functionHandlers1, IFunctionHandler[] functionHandlers2)
        {
            if (functionHandlers1 == null)
            {
                throw new ArgumentException(nameof(functionHandlers1));
            }
            if (functionHandlers2 == null)
            {
                throw new ArgumentException(nameof(functionHandlers2));
            }
            FunctionHandlers = functionHandlers1
                .Where(fh1 => functionHandlers2.All(fh2 => fh2.GetType() != fh1.GetType()))
                .Concat(functionHandlers2)
                .Distinct()
                .ToArray();
        }
    }
}