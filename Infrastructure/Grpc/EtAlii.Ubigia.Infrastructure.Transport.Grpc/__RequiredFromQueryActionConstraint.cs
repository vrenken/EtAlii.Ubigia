//namespace EtAlii.Ubigia.Infrastructure.Transport.Grpc
//[
//    using Microsoft.Grpc.Mvc.ActionConstraints

//    public class RequiredFromQueryActionConstraint : IActionConstraint
//    [
//        private readonly string _parameter

//        public RequiredFromQueryActionConstraint(string parameter)
//        [
//            _parameter = parameter
//        ]
//        public int Order => 999

//        public bool Accept(ActionConstraintContext context)
//        [
//            if (!context.RouteContext.HttpContext.Request.Query.ContainsKey(_parameter))
//            [
//                return false
//            ]
//            return true
//        ]
//    ]
//]