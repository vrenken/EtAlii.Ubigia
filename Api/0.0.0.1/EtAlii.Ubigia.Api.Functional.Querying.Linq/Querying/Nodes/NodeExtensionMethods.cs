namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System.Reflection;

    public static class NodeExtensionMethod
    {
        // ReSharper disable InconsistentNaming
        public static readonly MethodInfo Select = typeof(QueryableNodeExtensions).GetTypeInfo().GetDeclaredMethod("Select");
        public static readonly MethodInfo Add = typeof(QueryableNodeExtensions).GetTypeInfo().GetDeclaredMethod("Add");
        public static readonly MethodInfo Latest = typeof(QueryableNodeExtensions).GetTypeInfo().GetDeclaredMethod("Latest");
        public static readonly MethodInfo At = typeof(QueryableNodeExtensions).GetTypeInfo().GetDeclaredMethod("At");
        // ReSharper restore InconsistentNaming
    }
}