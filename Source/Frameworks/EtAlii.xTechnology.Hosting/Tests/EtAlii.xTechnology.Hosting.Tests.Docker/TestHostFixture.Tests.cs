// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Docker
{
    using System.Threading.Tasks;
    using Xunit;

    public class TestHostFixtureTests : IClassFixture<TestHostFixture>
    {
        private readonly TestHostFixture _fixture;
        
        public TestHostFixtureTests(TestHostFixture fixture)
        {
            _fixture = fixture;
            _fixture.Start().Wait();
        }

        [Fact(Skip = "Not implemented (yet)")]
        public async Task TestHostFixture_IsReachable()
        {
            Assert.True(_fixture.ContainerStarting);
            Assert.True(_fixture.ContainerStarted);

            await Task.CompletedTask.ConfigureAwait(false);
            // var connection = new SqlConnection(
            //     $"Server=localhost,{this.fixture.Ports.Single()};Database=master;TrustServerCertificate=True;User Id=sa;Password={this.fixture.Options.SaPassword}");
            //
            // await connection.OpenAsync().ConfigureAwait(false);
            // var cmd = new SqlCommand("SELECT 1+1", connection);
            // var result = await cmd.ExecuteScalarAsync().ConfigureAwait(false);
            //
            // Assert.Equal(2,result);
        }
    }
}