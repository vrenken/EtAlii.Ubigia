namespace EtAlii.Ubigia.Provisioning.Tests
{
    public static class TestAssembly
    {
        ///// 
        ///// Our one-time initialization method called at the very start of running unit tests in this assembly
        /////
        /////
        //[AssemblyInitialize]
        //public static void Initialize(TestContext context)
        //{
        //    var type = typeof(TestAssembly);
        //    var assembly = type.Assembly;
        //    var assemblyTitle = ((AssemblyTitleAttribute)assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute)).First()).Title;
        //    var assemblyVersion = assembly.GetName().Version;
        //    var productName = ((AssemblyProductAttribute)assembly.GetCustomAttributes(typeof(AssemblyProductAttribute)).First()).Product;

        //    // Needed for async/await troubleshooting.
        //    //AppDomain.CurrentDomain.UnhandledException += (sender, args) => { throw (Exception)args.ExceptionObject; };

        //    var diagnostics = TestDiagnostics.Create();
        //    if(diagnostics.EnableLogging || diagnostics.EnableProfiling)
        //    {
        //        // Start logging.
        //        //Logger.StartSession(productName, assemblyTitle, assemblyVersion); // Disabled because of performance loss.
        //    }
        //}

        ///// 
        ///// Our one-time cleanup method called at the very end of running unit tests in this assembly
        /////
        //[AssemblyCleanup]
        //public static void Cleanup()
        //{
        //    // End logging.
        //    var diagnostics = TestDiagnostics.Create();
        //    if (diagnostics.EnableLogging || diagnostics.EnableProfiling)
        //    {
        //        //Logger.EndSession(); // Disabled because of performance loss.
        //    }
        //}

        public const string Category = "EtAlii.Ubigia.Provisioning";
    }
}