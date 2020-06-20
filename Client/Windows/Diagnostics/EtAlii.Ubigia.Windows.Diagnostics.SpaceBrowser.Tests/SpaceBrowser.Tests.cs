namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser.Tests
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Markup;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Fabric.Tests;
    using EtAlii.Ubigia.Api.Transport.Diagnostics;
    using EtAlii.Ubigia.Windows.Client;
    using EtAlii.Ubigia.Windows.Settings;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;
    using App = EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser.App;
    using MainWindow = EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser.MainWindow;

    public class SpaceBrowserTests  : IClassFixture<FabricUnitTestContext>
    {
        private readonly FabricUnitTestContext _testContext;

        public SpaceBrowserTests(FabricUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public void SpaceBrowser_App_Create()
        {
            // Arrange.
            
            // Act.
            var app = new App();
            
            // Assert.
            Assert.NotNull(app);
            Assert.NotNull(App.Current);
            Assert.Null(app.MainWindow);
        }
        
        [Fact]
        public void SpaceBrowser_StorageSettingsViewModel_Create()
        {
            // Arrange.
            var globalSettings = new GlobalSettings();
            
            // Act.
            var vm = new StorageSettingsViewModel(globalSettings);
            
            // Assert.
            Assert.NotNull(vm);
            Assert.Equal(globalSettings, vm.GlobalSettings);
            Assert.Null(vm.StorageSettings);
        }
        
        [Fact]
        public async Task SpaceBrowser_StorageWindow_Create()
        {
            await StaHelper.StartStaTask(() =>
            {
                // Arrange.
                StorageWindow window = null;

                // Act.
                var act = new Action(() => window = new StorageWindow());

                // Assert.
                Assert.Throws<XamlParseException>(act);
                Assert.Null(window);
            });
        }
        
        [Fact]
        public async Task SpaceBrowser_MainWindow_Create_Without_ViewModel()
        {
            await StaHelper.StartStaTask(() =>
            {
                // Arrange.

                // Act.
                var window = new MainWindow();

                // Assert.
                Assert.NotNull(window);
            });
        }
        
        [Fact]
        public async Task SpaceBrowser_MainWindow_Create_With_ViewModel()
        {
            await StaHelper.StartStaTask(async () =>
            {
                // Arrange.
                using var fabric = await _testContext.FabricTestContext.CreateFabricContext(true);
                var diagnostics = new DiagnosticsFactory().CreateDisabled("EtAlii", "EtAlii.Ubigia.SpaceBrowser");
                var profiler = new Profiler(ProfilingAspects.Transport.Connection);
                
                // Act.
                var window = new MainWindowFactory().Create(new ProfilingDataConnection(fabric.Connection, profiler), diagnostics); 
                
                // Assert.
                Assert.NotNull(window);
                Assert.NotNull(window.ViewModel);
            });
        }
        
        [Fact]
        public async Task SpaceBrowser_RootsViewModel_Create()
        {
            await StaHelper.StartStaTask(async () =>
            {
                // Arrange.
                using var fabric = await _testContext.FabricTestContext.CreateFabricContext(true);
                
                // Act.
                var vm = new RootsViewModel(fabric); 
                
                // Assert.
                Assert.NotNull(vm);
                Assert.NotNull(vm.BeginEntryDragCommand);
                Assert.NotEmpty(vm.AvailableRoots);
                Assert.Null(vm.SelectedRoot);
            });
        }
        
        [Fact]
        public async Task SpaceBrowser_JournalViewModel_Create()
        {
            await StaHelper.StartStaTask(async () =>
            {
                // Arrange.
                using var fabric = await _testContext.FabricTestContext.CreateFabricContext(true);
                var mainDispatcherInvoker = new MainDispatcherInvoker();
                
                // Act.
                var vm = new JournalViewModel(fabric, mainDispatcherInvoker); 
                
                // Assert.
                Assert.NotNull(vm);
                Assert.Empty(vm.Items);
                Assert.Equal(300, vm.Size);
            });
        }
    }
}