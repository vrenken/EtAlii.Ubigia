//namespace EtAlii.Servus.Api.Functional
//{
//    using EtAlii.xTechnology.Structure;

//    internal class PathSubjectConverterSelector : Selector2<SequencePart, SequencePart, IPathSubjectConverter>, IPathSubjectConverterSelector
//    {
//        public PathSubjectConverterSelector(
//            IPathSubjectForVariableAssignmentOperationConverter pathSubjectForVariableAssignmentOperationConverter,
//            IPathSubjectForVariableAddOperationConverter pathSubjectForVariableAddOperationConverter,
//            IPathSubjectForPathAssignmentOperationConverter pathSubjectForPathAssignmentOperationConverter,
//            IPathSubjectForPathAddOperationConverter pathSubjectForPathAddOperationConverter,
//            IPathSubjectForPathRemoveOperationConverter pathSubjectForPathRemoveOperationConverter,
//            IPathSubjectForOutputConverter pathSubjectForOutputConverter,
//            IPathSubjectForFunctionAssignmentOperationConverter pathSubjectForFunctionAssignmentOperationConverter)
//        {
//            this.Register((futurePart, leftPart) => futurePart is VariableSubject && leftPart is AssignOperator, pathSubjectForVariableAssignmentOperationConverter)
//                .Register((futurePart, leftPart) => futurePart is VariableSubject && leftPart is AddOperator, pathSubjectForVariableAddOperationConverter)
//                .Register((futurePart, leftPart) => futurePart is PathSubject && leftPart is AssignOperator, pathSubjectForPathAssignmentOperationConverter)
//                .Register((futurePart, leftPart) => futurePart is PathSubject && leftPart is AddOperator, pathSubjectForPathAddOperationConverter)
//                .Register((futurePart, leftPart) => futurePart is PathSubject && leftPart is RemoveOperator, pathSubjectForPathRemoveOperationConverter)
//                .Register((futurePart, leftPart) => futurePart == null && leftPart is AddOperator, pathSubjectForPathAddOperationConverter)
//                .Register((futurePart, leftPart) => futurePart == null && leftPart is AssignOperator, pathSubjectForOutputConverter)
//                .Register((futurePart, leftPart) => futurePart == null && leftPart == null, pathSubjectForOutputConverter)
//                .Register((futurePart, leftPart) => futurePart is FunctionSubject && leftPart is AssignOperator, pathSubjectForFunctionAssignmentOperationConverter);
//        }
//    }
//}
