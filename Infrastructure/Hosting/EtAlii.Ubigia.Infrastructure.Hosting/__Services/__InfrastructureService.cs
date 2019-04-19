//namespace EtAlii.Ubigia.Infrastructure.Hosting
//[
//    using System
//    using System.Text
//    using EtAlii.Ubigia.Infrastructure.Functional
//    using EtAlii.xTechnology.Hosting

//    public class InfrastructureService : ServiceBase, IInfrastructureService
//    [
//        private readonly IInfrastructure _infrastructure

//        public InfrastructureService(IInfrastructure infrastructure)
//        [
//            _infrastructure = infrastructure
//        ]
//        public override void Start()
//        [
//            Status.Title = "Ubigia infrastructure"

//            Status.Description = "Starting..."
//            Status.Summary = Status.Description

//            _infrastructure.Start()

//            var sb = new StringBuilder()
//            sb.AppendLine("All OK. Ubigia is serving the storage specified below.")
//            sb.AppendLine($"Name: {_infrastructure.Configuration.Name}")
//            sb.AppendLine($"Address: {_infrastructure.Configuration.Address}")
//            Status.Summary = sb.ToString()
//            Status.Summary = Status.Description
//        ]
//        public override void Stop()
//        [
//            Status.Description = "Stopping..."
//            Status.Summary = Status.Description

//            _infrastructure.Stop()

//            Status.Description = "Stopped."
//            Status.Summary = Status.Description
//        ]
//	    protected override void Initialize(IHost host, ISystem system, IModule[] moduleChain, out Status status)
//	    [
//			status = new Status(nameof(InfrastructureService))
//		]
//    ]
//]