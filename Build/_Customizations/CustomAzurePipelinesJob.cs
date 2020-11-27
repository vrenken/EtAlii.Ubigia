namespace EtAlii.Ubigia.Pipelines
{
    using System.Linq;
    using Nuke.Common.CI.AzurePipelines.Configuration;
    using Nuke.Common.Tooling;
    using Nuke.Common.Utilities;

    public class CustomAzurePipelinesJob : AzurePipelinesJob
    {
        public int TimeoutInMinutes { get; set; } = 60;
        public CustomAzurePipelinesJob(AzurePipelinesJob job)
        {
            Name = job.Name;
            DisplayName = job.Name;
            BuildCmdPath = job.BuildCmdPath;
            Dependencies = job.Dependencies;
            Parallel = job.Parallel;
            PartitionName = job.PartitionName;
            Imports = job.Imports;
            InvokedTargets = job.InvokedTargets;
            PublishArtifacts = job.PublishArtifacts;
        }

        public override void Write(CustomFileWriter writer)
        {
            using (writer.WriteBlock($"- job: {Name}"))
            {
                writer.WriteLine($"displayName: {DisplayName.SingleQuote()}");
                writer.WriteLine($"dependsOn: [ {Dependencies.Select(x => x.Name).JoinComma()} ]");

                writer.WriteLine($"timeoutInMinutes: {TimeoutInMinutes}");

                if (Image != null)
                {
                    using (writer.WriteBlock("pool:"))
                    {
                        writer.WriteLine($"vmImage: {Image.Value.GetValue().SingleQuote().SingleQuote()}");
                    }
                }

                if (Parallel > 1)
                {
                    using (writer.WriteBlock("strategy:"))
                    {
                        writer.WriteLine($"parallel: {Parallel}");
                    }
                }

                using (writer.WriteBlock("steps:"))
                {
                    WriteSteps(writer);
                }
            }
        }
    }
}