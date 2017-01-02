namespace EtAlii.Ubigia.Client.Windows.ShellExtension
{
    using EtAlii.Ubigia.Client.Windows.Shared;
    using LogicNP.EZNamespaceExtensions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.InteropServices;

    // The GUID of the class representing the root folder is used as the 
    // GUID for the namespace extension
    [Guid(Identifiers.ShellExtensionRegistrationString)]
    public class Registration
    {
        //static Registration() 
        //{ 
        //    AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve); 
        //} 

        //static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) 
        //{
        //    Debugger.Launch();
        //    if (args.Name.Contains("Dependant")) // .Net runtime is trying to load Dependant.dll assembly 
        //    { 
        //        // Load it from the same path as our nse dll and return it 
        //        string path = Assembly.GetExecutingAssembly().Location; 
        //        path = Path.GetDirectoryName(path);
        //        path = Path.Combine(App.CurrentDirectory, "Dependant.dll"); 
        //        Assembly ret = Assembly.LoadFrom(path); 
        //        return ret; 
        //    } 

        //    return null; 
        //}

        // Your obsoleteRegistration should have one static method marked with the 
        // ComRegisterFunction attribute. The function should return void and take 
        // one parameter whose type is System.Type.
        // 
        // This method is called when you register the namespace extension obsoleteRegistration dll file
        // using the RegisterExtensionDotNetXX.exe utility
        [ComRegisterFunction]
        private static void Register(Type type)
        {
            try
            {
                var globalSettings = App.Current.Container.GetInstance<GlobalSettings>();

                var obsoleteRegistrations = Registrations.GetObsolete(globalSettings);
                //var obsoleteRegistrations = GetAllRegistrations(globalSettings);

                foreach (var obsoleteRegistration in obsoleteRegistrations)
                {
                    File.Delete(obsoleteRegistration);
                }

                var missingRegistrations = Registrations.GetMissing(globalSettings);

                foreach (var missingRegistration in missingRegistrations)
                {
                    CreateShellExtensionAssembly(missingRegistration);
                }

                var shellExtensionTypes = GetAllShellExtensionTypes();
                foreach (var shellExtensionType in shellExtensionTypes)
                {
                    NSEFolder.RegisterExtension(shellExtensionType);
                }
            }
            catch (Exception)
            {
                Debugger.Launch();
            }
        }

        // Your obsoleteRegistration should have one static method marked with the 
        // ComUnregisterFunction attribute. The function should return void and take 
        // one parameter whose type is System.Type.
        // 
        // This method is called when you unregister the namespace extension obsoleteRegistration dll file
        // using the RegisterExtensionDotNetXX.exe utility
        [ComUnregisterFunction]
        private static void Unregister(Type type)
        {
            try
            {
                var shellExtensionTypes = GetAllShellExtensionTypes();
                foreach (var shellExtensionType in shellExtensionTypes)
                {
                    NSEFolder.UnregisterExtension(shellExtensionType);
                }
            }
            catch (Exception)
            {
                Debugger.Launch();
            }
        }

        private static Type[] GetAllShellExtensionTypes()
        {
            var shellExtensionTypes = new List<Type>();

            var rootFolderItemType = typeof(RootFolderItem);

            var assemblyFiles = Directory.GetFiles(App.ShellExtensionsDirectory, "*.dll");
            foreach (var assemblyFile in assemblyFiles)
            {
                var assembly = Assembly.LoadFile(assemblyFile);
                var shellExtensionType = assembly.GetTypes()
                                                 .Where(type => type.IsClass && !type.IsAbstract)
                                                 .Where(type => type.IsSubclassOf(rootFolderItemType))
                                                 .Single();
                shellExtensionTypes.Add(shellExtensionType);
            }
            return shellExtensionTypes.ToArray();
        }

        private static string[] GetAllRegistrations(GlobalSettings globalSettings)
        {
            var assemblies = Directory.GetFiles(App.ShellExtensionsDirectory, "*.dll");
            return assemblies.ToArray();
        }


        private static void CreateShellExtensionAssembly(Guid shellExtensionId)
        {
            string displayName = shellExtensionId.ToString();
            var assemblyFileName = String.Format("{0}.dll", shellExtensionId);

            var assemblyName = new AssemblyName(displayName);

            //using (var fileStream = new FileStream("ShellRegistration.snk", FileMode.Open))
            //{
            //    var strongNameKeyPair = new StrongNameKeyPair(fileStream);
            //    assemblyName.KeyPair = strongNameKeyPair;
            //}

            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave, App.ShellExtensionsDirectory);

            // For a single-module obsoleteRegistration, the module name is usually 
            // the obsoleteRegistration name plus an extension.
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name, assemblyFileName);

            var rootFolderItemType = typeof(RootFolderItem);
            var typeName = String.Format("{0}_{1}", rootFolderItemType.Name, displayName.Replace("-", ""));
            var typeBuilder = moduleBuilder.DefineType(typeName, TypeAttributes.Public, rootFolderItemType);
            
            var constructorParameters = new Type[] { typeof(string) };
	        var guidClassConstructorInfo = typeof(GuidAttribute).GetConstructor(constructorParameters);

            var guidAttributeBuilder = new CustomAttributeBuilder(guidClassConstructorInfo, new object[] { shellExtensionId.ToString() });
            typeBuilder.SetCustomAttribute(guidAttributeBuilder);

            // Define a constructor.
            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);

            typeBuilder.CreateType();

            // The following line saves the single-module obsoleteRegistration. This 
            // requires AssemblyBuilderAccess to include Save. You can now 
            // type "ildasm MyDynamicAsm.dll" at the command prompt, and 
            // examine the obsoleteRegistration. You can also write a program that has 
            // a reference to the obsoleteRegistration, and use the MyDynamicType type. 
            // 
            assemblyBuilder.Save(assemblyFileName);
        }
    }
}
