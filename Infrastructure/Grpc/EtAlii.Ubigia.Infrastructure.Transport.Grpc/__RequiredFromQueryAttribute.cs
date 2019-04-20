//namespace EtAlii.Ubigia.Infrastructure.Transport.Grpc
//[
//    using System.Linq
//    using Microsoft.Grpc.Mvc
//    using Microsoft.Grpc.Mvc.ApplicationModels
    
//    public class RequiredFromQueryAttribute : FromQueryAttribute, IParameterModelConvention
//    [
//        public void Apply(ParameterModel parameter)
//        [
//            if [parameter.Action.Selectors != null && parameter.Action.Selectors.Any[]]
//            [
//                parameter.Action.Selectors.Last().ActionConstraints.Add(new RequiredFromQueryActionConstraint(parameter.BindingInfo?.BinderModelName ?? parameter.ParameterName))
//            ]
//        ]
//    ]
//]