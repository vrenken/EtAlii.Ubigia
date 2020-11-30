namespace EtAlii.Ubigia.Pipelines
{
    using System.Linq;
    using Nuke.Common.CI.AzurePipelines.Configuration;
    using Nuke.Common.Tooling;
    using Nuke.Common.Utilities;
    using Nuke.Common.Utilities.Collections;

    public class CustomAzurePipelinesStage : AzurePipelinesStage
    {
        public string Pool { get; set; }

        public CustomAzurePipelinesStage(AzurePipelinesStage stage)
        {
            Name = stage.Name;
            DisplayName = stage.DisplayName;
            Image = stage.Image;
            Dependencies = stage.Dependencies;
            Jobs = stage.Jobs;
        }
        
        public override void Write(CustomFileWriter writer)
        {
            using (writer.WriteBlock($"- stage: {Name}"))
            {
                writer.WriteLine($"displayName: {DisplayName.SingleQuote()}");
                writer.WriteLine($"dependsOn: [ {Dependencies.Select(x => x.Name).JoinComma()} ]");

                if (Pool != null)
                {
                    using (writer.WriteBlock("pool:"))
                    {
                        writer.WriteLine($"name: {Pool.SingleQuote()}");
                    }
                }
                else if (Image != null)
                {
                    using (writer.WriteBlock("pool:"))
                    {
                        writer.WriteLine($"vmImage: {Image.Value.GetValue().SingleQuote()}");
                    }
                }

                using (writer.WriteBlock("jobs:"))
                {
                    Jobs.ForEach(x => x.Write(writer));
                }
            }
        }
    }
}