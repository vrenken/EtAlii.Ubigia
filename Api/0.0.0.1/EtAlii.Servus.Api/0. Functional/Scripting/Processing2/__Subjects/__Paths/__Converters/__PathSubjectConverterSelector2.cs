﻿//namespace EtAlii.Servus.Api.Functional
//{
//    using EtAlii.xTechnology.Structure;

//    internal class PathSubjectConverterSelector2 : Selector2<SequencePart, SequencePart, IPathSubjectConverter2>, IPathSubjectConverterSelector2
//    {
//        public PathSubjectConverterSelector2(
//            IPathSubjectForVariableAssignmentOperationConverter2 pathSubjectForVariableAssignmentOperationConverter,
//            IPathSubjectForVariableAddOperationConverter2 pathSubjectForVariableAddOperationConverter,
//            IPathSubjectForPathAssignmentOperationConverter2 pathSubjectForPathAssignmentOperationConverter,
//            IPathSubjectForPathAddOperationConverter2 pathSubjectForPathAddOperationConverter,
//            IPathSubjectForPathRemoveOperationConverter2 pathSubjectForPathRemoveOperationConverter,
//            IPathSubjectForOutputConverter2 pathSubjectForOutputConverter,
//            IPathSubjectForFunctionAssignmentOperationConverter2 pathSubjectForFunctionAssignmentOperationConverter)
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
