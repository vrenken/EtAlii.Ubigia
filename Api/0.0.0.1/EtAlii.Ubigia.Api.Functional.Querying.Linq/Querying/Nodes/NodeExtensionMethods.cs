namespace EtAlii.Ubigia.Api.Functional
{
    using System.Reflection;

    public static class NodeExtensionMethod
    {
        public static readonly MethodInfo Select = typeof(QueryableNodeExtensions).GetTypeInfo().GetDeclaredMethod("Select");
        public static readonly MethodInfo Add = typeof(QueryableNodeExtensions).GetTypeInfo().GetDeclaredMethod("Add");
        public static readonly MethodInfo Latest = typeof(QueryableNodeExtensions).GetTypeInfo().GetDeclaredMethod("Latest");
        public static readonly MethodInfo At = typeof(QueryableNodeExtensions).GetTypeInfo().GetDeclaredMethod("At");
    }
}