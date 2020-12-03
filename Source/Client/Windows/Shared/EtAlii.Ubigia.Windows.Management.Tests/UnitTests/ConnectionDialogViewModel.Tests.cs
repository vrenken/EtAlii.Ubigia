namespace EtAlii.Ubigia.Windows.Management.Tests
{
    using System.Threading.Tasks;
    using Xunit;

    public class ConnectionDialogViewModelTests
    {
        [Fact]
        public async Task ConnectionDialogViewModel_Create()
        {
            await StaHelper.StartStaTask(() =>
            {
                // Arrange.

                // Act.
                var viewModel = new ConnectionDialogViewModel(new ConnectionDialogWindow(), "http://nohost", "nouser", "nopassword");

                // Assert.
                Assert.NotNull(viewModel);
            }).ConfigureAwait(false);
        }
    }
}