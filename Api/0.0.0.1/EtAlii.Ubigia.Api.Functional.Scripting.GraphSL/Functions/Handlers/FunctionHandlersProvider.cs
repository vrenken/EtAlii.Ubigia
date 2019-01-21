namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    public class FunctionHandlersProvider : IFunctionHandlersProvider
    {
        public IFunctionHandler[] FunctionHandlers { get; }

        public static readonly IFunctionHandlersProvider Empty = new FunctionHandlersProvider(new IFunctionHandler[] {});

        public FunctionHandlersProvider(IFunctionHandler[] functionHandlers)
        {
            FunctionHandlers = functionHandlers ?? throw new ArgumentException(nameof(functionHandlers));
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