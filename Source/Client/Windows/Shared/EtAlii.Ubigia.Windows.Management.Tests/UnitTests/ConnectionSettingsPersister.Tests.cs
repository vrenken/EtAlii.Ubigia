namespace EtAlii.Ubigia.Windows.Management.Tests
{
    using System.Threading.Tasks;
    using Xunit;

    public class ConnectionSettingsPersisterTests
    {
        [Fact]
        public async Task ConnectionSettingsPersister_Create()
        {
            await StaHelper.StartStaTask(() =>
            {
                // Arrange.
                var viewModel = new ConnectionDialogViewModel(new ConnectionDialogWindow(), "http://nohost", "nouser", "nopassword");

                // Act.
                var connectionSettingsPersister = new ConnectionSettingsPersister(viewModel);

                // Assert.
                Assert.NotNull(connectionSettingsPersister);
            }).ConfigureAwait(false);
        }
    }
}