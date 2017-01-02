namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using EtAlii.xTechnology.MicroContainer;

    internal static class ScriptParserFactory
    {
        public static IScriptParser Create()
        {
            var container = new Container();
            container.Register<IScriptParser, ScriptParser>(Lifestyle.Singleton);
            container.Register<ActionsParser>(Lifestyle.Singleton);
            container.Register<PathParser>(Lifestyle.Singleton);
            container.Register<VariableParser>(Lifestyle.Singleton);
            container.Register<IdentifierParser>(Lifestyle.Singleton);
            container.Register<IParserHelper, ParserHelper>(Lifestyle.Singleton);

            container.Register<AddItemActionParser>(Lifestyle.Singleton);
            container.Register<RemoveItemActionParser>(Lifestyle.Singleton);
            container.Register<UpdateItemActionParser>(Lifestyle.Singleton);
            container.Register<ItemOutputActionParser>(Lifestyle.Singleton);
            container.Register<ItemsOutputActionParser>(Lifestyle.Singleton);
            container.Register<VariableAddItemActionParser>(Lifestyle.Singleton);
            container.Register<VariableUpdateItemHandler>(Lifestyle.Singleton);
            container.Register<VariableItemAssignmentActionParser>(Lifestyle.Singleton);
            container.Register<VariableItemsAssignmentActionParser>(Lifestyle.Singleton);
            container.Register<VariableStringAssignmentActionParser>(Lifestyle.Singleton);
            container.Register<VariableOutputActionParser>(Lifestyle.Singleton);

            return container.GetInstance<IScriptParser>();
        }
    }
}
