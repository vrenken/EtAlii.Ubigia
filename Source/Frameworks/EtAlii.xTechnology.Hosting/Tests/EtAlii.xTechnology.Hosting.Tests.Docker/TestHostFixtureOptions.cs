// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Docker
{
    using System;
    using System.IO;
    using DockerizedTesting;
    using DockerizedTesting.ImageProviders;

    public class TestHostFixtureOptions : FixtureOptions
    {
        public override IDockerImageProvider ImageProvider { get; }
        public string TestUserName { get; set; } = "D0cK3rIz3d_T3sting!!";
        public string TestPassword { get; set; } = "D0cK3rIz3d_T3sting!!";

        public override int DelayMs => 2000;

        public TestHostFixtureOptions() 
        {
            var dockerFileFolder = Path.Combine(Environment.CurrentDirectory, "Container");
            var dockerFile = Path.Combine(dockerFileFolder, "dockerfile");
            ImageProvider = new DockerfileImageProvider(new FileInfo(dockerFile), dockerFileFolder);
        }
    }
}