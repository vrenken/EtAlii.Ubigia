namespace EtAlii.Ubigia.Api.Data.Tests
{
    using EtAlii.xTechnology.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using System.Reflection;

    [TestClass] //you have to label the class with this or it is never scanned for methods
    public class TestAssembly
    {
        /// 
        /// Our one-time initialization method called at the very start of running unit tests in this assembly
        ///
        ///
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            var type = typeof(TestAssembly);
            var assembly = type.Assembly;
            var assemblyTitle = ((AssemblyTitleAttribute)assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute)).First()).Title;
            var assemblyVersion = assembly.GetName().Version;
            var productName = ((AssemblyProductAttribute)assembly.GetCustomAttributes(typeof(AssemblyProductAttribute)).First()).Product;

            // Start logging.
            Logger.StartSession(productName, assemblyTitle, assemblyVersion);
        }

        /// 
        /// Our one-time cleanup method called at the very end of running unit tests in this assembly
        ///
        [AssemblyCleanup]
        public static void Cleanup()
        {
            // End logging.
            Logger.EndSession();
        }

        public const string Category = "EtAlii.Ubigia.Api.Data";
    }
}