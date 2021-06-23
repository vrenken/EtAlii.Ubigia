namespace EtAlii.xTechnology.Hosting.Tests.Docker
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DockerizedTesting;
    using global::Docker.DotNet.Models;

    public class TestHostFixture : BaseFixture<TestHostFixtureOptions>
    {
        private const string LocalImageName = "etalii-xtechnology-hosting-dockerhost:latest";
        public TestHostFixture() : base("EtAlii.xTechnology.Hosting.Test", 1)
        {
        }

        public Task Start() => Start(new TestHostFixtureOptions());

        public override async Task Start(TestHostFixtureOptions options)
        {
            //await CreateImageWhenNeeded().ConfigureAwait(false);
            await base.Start(options).ConfigureAwait(false);
        }

        // private async Task CreateImageWhenNeeded()
        // {
        //     var dockerUri = new Uri(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
        //         ? "npipe://./pipe/docker_engine"
        //         : "unix:/var/run/docker.sock");
        //     using var client = new DockerClientConfiguration(dockerUri).CreateClient();
        //
        //     // Let's cleanup old images.
        //     await client.Images.PruneImagesAsync().ConfigureAwait(false);
        //
        //     var parameters = new ImageBuildParameters { Tags = new[] { LocalImageName} };
        //     var containerFolder = Path.Combine(Environment.CurrentDirectory, "Container");
        //     var tarballStream = TarballStream.CreateFromFolder(containerFolder);
        //     var result = client.Images.BuildImageFromDockerfileAsync(tarballStream, parameters);
        //
        //     var progress = new Progress<JSONMessage>(m =>
        //     {
        //         if(m.ErrorMessage != null) Trace.WriteLine(m.ErrorMessage);
        //         if(m.ProgressMessage != null) Trace.WriteLine(m.ProgressMessage);
        //     });
        //     await StreamUtil.MonitorStreamForMessagesAsync(result, client, CancellationToken.None, progress).ConfigureAwait(false);
        //
        // }
        protected override CreateContainerParameters GetContainerParameters(int[] ports)
        {
            const int port = 1433;

            var hostPorts = ports;
            var containerPorts = new[] {port};

            // var applicationFolder = Path.Combine(currentFolder, "../../../../EtAlii.xTechnology.Hosting.Tests.Console/bin/debug/netcoreapp3.1");

            var portBindings = new Dictionary<string, IList<PortBinding>>
            {
                {
                    port.ToString(), new[]
                    {
                        new PortBinding
                        {
                            HostPort = hostPorts[Array.IndexOf(containerPorts, port)].ToString()
                        }
                    }
                }
            };
            // var portBindings = containerPorts
            //     .ToDictionary(
            //         k => k.ToString(),
            //         v => (IList<PortBinding>) new[]
            //         {
            //             new PortBinding
            //             {
            //                 HostPort = hostPorts[Array.IndexOf(containerPorts, v) % hostPorts.Length].ToString()
            //             }
            //         });
            return new CreateContainerParameters
            {
                AttachStdout = true,
                AttachStdin = true,
                AttachStderr = true,
                OpenStdin = true,
                Image = LocalImageName,
                HostConfig = new HostConfig
                {
                    AutoRemove = true,
                    PortBindings = portBindings,
                    // Mounts = new[]
                    // {
                    //     new Mount
                    //     {
                    //         Source = applicationFolder,
                    //         Target = "/app",
                    //         Type = "bind"
                    //     }
                    // }
                },
                WorkingDir = "/app",
                ExposedPorts = new Dictionary<string, EmptyStruct> {{ port.ToString(), default}},
                Env = new List<string>(new[]
                {
                    $"USER={Options.TestUserName}",
                    $"PASSWORD={Options.TestPassword}",
                }),
            };
        }

        protected override async Task<bool> IsContainerRunning(int[] ports)
        {
            try
            {
                await Task.CompletedTask.ConfigureAwait(false);//Task.Delay(TimeSpan.FromMinutes(1));// Task.CompletedTask.ConfigureAwait(false);
                // var connectionString = GetMsSqlConnectionString(ports.Single());
                // var connection = new SqlConnection(connectionString);
                // await connection.OpenAsync().ConfigureAwait(false);
                // var cmd = new SqlCommand("SELECT GETDATE()", connection);
                // await cmd.ExecuteScalarAsync().ConfigureAwait(false);
                return false;
            }
            catch
            {
                return false;
            }
        }

    }
}
