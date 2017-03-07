namespace EtAlii.Ubigia.Api.Functional
{
    using System.Reflection;

    public static class NodeExtensionMethod
    {
        public static readonly MethodInfo Select;
        public static readonly MethodInfo Add;
        public static readonly MethodInfo Latest;
        public static readonly MethodInfo At;

        static NodeExtensionMethod()
        {
            var typeInfo = typeof(QueryableNodeExtensions).GetTypeInfo();
            Select = typeInfo.GetDeclaredMethod("Select");
            Add = typeInfo.GetDeclaredMethod("Add");
            Latest = typeInfo.GetDeclaredMethod("Latest");
            At = typeInfo.GetDeclaredMethod("At");
        }
    }
}