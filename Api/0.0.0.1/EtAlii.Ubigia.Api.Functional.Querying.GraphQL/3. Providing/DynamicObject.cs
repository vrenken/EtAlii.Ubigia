namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    public static class DynamicObject
    {
        private static readonly ModuleBuilder ModuleBuilder = CreateModuleBuilder(); 

        private static ModuleBuilder CreateModuleBuilder()
        {
            var assemblyName = new AssemblyName($"DynamicAssembly_{Guid.NewGuid():N}");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            return assemblyBuilder.DefineDynamicModule($"DynamicModule_{nameof(DynamicObject)}");
        }

        public static object CreateInstance(PropertyDictionary properties)
        {
            var typeName = $"{typeof(DynamicObject).Name}_{Guid.NewGuid():N}";
            var typeBuilder = ModuleBuilder.DefineType(typeName, TypeAttributes.Public, typeof(object));

            foreach (var kvp in properties)
            {
                AddProperty(typeBuilder, kvp.Key, kvp.Value.GetType());
            }
            var typeInfo = typeBuilder.CreateTypeInfo();
            var instance = Activator.CreateInstance(typeInfo);

            foreach (var propertyInfo in typeInfo.DeclaredProperties)
            {
                var propertyName = propertyInfo.Name;
                var propertyValue = properties[propertyName];
                propertyInfo.SetValue(instance, propertyValue);
            }

            return instance;
        }

        private static void AddProperty(TypeBuilder typeBuilder, string propertyName, System.Type propertyType)
        {
            var fieldBuilder = typeBuilder.DefineField($"_{propertyName}", propertyType, FieldAttributes.Private);

            // The last argument of DefineProperty is null, because the
            // property has no parameters. (If you don't specify null, you must
            // specify an array of Type objects. For a parameterless property,
            // use an array with no elements: new Type[] [])
            var propertyBuilder = typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);

            // The property set and property get methods require a special
            // set of attributes.
            var getSetMethodAttributes = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
    
            // Define the "get" accessor method for CustomerName.
            var getMethodBuilder = typeBuilder.DefineMethod($"get_{propertyName}", getSetMethodAttributes, propertyType, System.Type.EmptyTypes);
            var getMethodIlGenerator = getMethodBuilder.GetILGenerator();

            getMethodIlGenerator.Emit(OpCodes.Ldarg_0);
            getMethodIlGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
            getMethodIlGenerator.Emit(OpCodes.Ret);

            // Define the "set" accessor method for CustomerName.
            var setMethodBuilder = typeBuilder.DefineMethod($"set_{propertyName}", getSetMethodAttributes, null,new[] { propertyType });

            var setMethodIlGenerator = setMethodBuilder.GetILGenerator();

            setMethodIlGenerator.Emit(OpCodes.Ldarg_0);
            setMethodIlGenerator.Emit(OpCodes.Ldarg_1);
            setMethodIlGenerator.Emit(OpCodes.Stfld, fieldBuilder);
            setMethodIlGenerator.Emit(OpCodes.Ret);

            // Last, we must map the two methods created above to our PropertyBuilder to 
            // their corresponding behaviors, "get" and "set" respectively. 
            propertyBuilder.SetGetMethod(getMethodBuilder);
            propertyBuilder.SetSetMethod(setMethodBuilder);
        }
    }
}