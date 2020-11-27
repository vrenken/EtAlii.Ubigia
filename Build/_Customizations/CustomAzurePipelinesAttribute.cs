namespace EtAlii.Ubigia.Pipelines
{
    using System.Collections.Generic;
    using JetBrains.Annotations;
    using Nuke.Common.CI.AzurePipelines;
    using Nuke.Common.CI.AzurePipelines.Configuration;
    using Nuke.Common.Execution;
    using Nuke.Common.Tooling;

    public class CustomAzurePipelinesAttribute : AzurePipelinesAttribute
    {
        public int TimeoutInMinutes { get; set; } = 60;
        public CustomAzurePipelinesAttribute(AzurePipelinesImage image, params AzurePipelinesImage[] images) : base(image, images)
        {
        }

        public CustomAzurePipelinesAttribute([CanBeNull] string suffix, AzurePipelinesImage image, params AzurePipelinesImage[] images) : base(suffix, image, images)
        {
        }

        protected override AzurePipelinesJob GetJob(
            ExecutableTarget executableTarget, 
            LookupTable<ExecutableTarget, AzurePipelinesJob> jobs, 
            IReadOnlyCollection<ExecutableTarget> relevantTargets)
        {
            var job = base.GetJob(executableTarget, jobs, relevantTargets);
            return new CustomAzurePipelinesJob(job)
            {
                TimeoutInMinutes = TimeoutInMinutes
            };
        }
    }
}