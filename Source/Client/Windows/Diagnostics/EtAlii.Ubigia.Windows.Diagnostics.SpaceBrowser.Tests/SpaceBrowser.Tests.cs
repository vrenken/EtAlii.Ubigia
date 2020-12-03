namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser.Tests
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Markup;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Api.Transport.Diagnostics;
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using EtAlii.Ubigia.Windows.Client;
    using EtAlii.Ubigia.Windows.Settings;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;
    using App = EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser.App;
    using MainWindow = EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser.MainWindow;

    public class SpaceBrowserTests  : IClassFixture<LogicalUnitTestContext>
    {
        private readonly LogicalUnitTestContext _testContext;

        public SpaceBrowserTests(LogicalUnitTestContext testContext)
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
            }).ConfigureAwait(false);
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
            }).ConfigureAwait(false);
        }
        
        [Fact]
        public async Task SpaceBrowser_MainWindow_Create_With_ViewModel()
        {
            // Arrange.
            using var fabric = await _testContext.FabricTestContext.CreateFabricContext(true).ConfigureAwait(false);
            var diagnostics = DiagnosticsConfiguration.Default;//= new DiagnosticsFactory().CreateDisabled("EtAlii", "EtAlii.Ubigia.SpaceBrowser");
            var profiler = new Profiler(ProfilingAspects.Transport.Connection);

            await StaHelper.StartStaTask(() =>
            {
                // Act.
                var window = new MainWindowFactory().Create(new ProfilingDataConnection(fabric.Connection, profiler), diagnostics); 
                
                // Assert.
                Assert.NotNull(window);
                Assert.NotNull(window.ViewModel);
            }).ConfigureAwait(false);
        }
        
        [Fact]
        public async Task SpaceBrowser_RootsViewModel_Create()
        {
            // Arrange.
            using var fabric = await _testContext.FabricTestContext.CreateFabricContext(true).ConfigureAwait(false);

            await StaHelper.StartStaTask(() =>
            {
                // Act.
                var vm = new RootsViewModel(fabric); 
                
                // Assert.
                Assert.NotNull(vm);
                Assert.NotNull(vm.BeginEntryDragCommand);
                Assert.NotEmpty(vm.AvailableRoots);
                Assert.Null(vm.SelectedRoot);
            }).ConfigureAwait(false);
        }
        
        [Fact(Skip = "Missing DocumentContext instance to fulfill test")]
        public async Task SpaceBrowser_FunctionalGraphDocumentFactory_Create()
        {
            // Arrange.
            using var fabric = await _testContext.FabricTestContext.CreateFabricContext(true).ConfigureAwait(false);

            await StaHelper.StartStaTask(() =>
            {
                // Act.
                var document = new FunctionalGraphDocumentFactory().Create(null); 
                
                // Assert.
                Assert.NotNull(document);
            }).ConfigureAwait(false);
        }

        [Fact(Skip = "Missing System.Reactive stuff to fulfill test")]
        public async Task SpaceBrowser_GraphView_Create()
        {
            // Arrange.

            await StaHelper.StartStaTask(() =>
            {
                // Act.
                var view = new GraphView(); 
                
                // Assert.
                Assert.NotNull(view);
            }).ConfigureAwait(false);
        }

        [Fact]
        public void SpaceBrowser_GraphScaffolding_Register()
        {
            // Arrange.
            var container = new Container();
            var scaffolding = new GraphScaffolding(); 

            // Act.
            scaffolding.Register(container);
            
            // Assert.
            Assert.NotNull(container);
        }
        
        [Fact(Skip = "Missing GraphContext instance to fulfill test")]
        public void SpaceBrowser_GraphScaffolding_Initialize()
        {
            // Arrange.
            var container = new Container();
            var scaffolding = new GraphScaffolding();
            scaffolding.Register(container);

            // Act.
            scaffolding.Initialize(container);
            
            // Assert.
            Assert.NotNull(container);
        }
        
        [Fact]
        public async Task SpaceBrowser_JournalViewModel_Create()
        {
            await StaHelper.StartStaTask(async () =>
            {
                // Arrange.
                using var fabric = await _testContext.FabricTestContext.CreateFabricContext(true).ConfigureAwait(false);
                var mainDispatcherInvoker = new MainDispatcherInvoker();
                
                // Act.
                var vm = new JournalViewModel(fabric, mainDispatcherInvoker); 
                
                // Assert.
                Assert.NotNull(vm);
                Assert.Empty(vm.Items);
                Assert.Equal(300, vm.Size);
            }).ConfigureAwait(false);
        }
    }
}