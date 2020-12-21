//namespace EtAlii.Ubigia.Api.Functional.Traversal
//[
//    using System
//    using System.Linq
//
//    internal class MediaByCompanyFamilyModelNumberHandler : IRootHandler
//    [
//        public PathSubjectPart[] Template [ get; ]
//
//        public MediaByCompanyFamilyModelNumberHandler()
//        [
//            // media:COMPANY/FAMILY/MODEL/NUMBER
//            Template = new PathSubjectPart[]
//            [
//                new TypedPathSubjectPart(TypedPathFormatter.Media.CompanyNameFormatter), new ParentPathSubjectPart(),
//                new TypedPathSubjectPart(TypedPathFormatter.Media.ProductFamilyNameFormatter), new ParentPathSubjectPart(),
//                new TypedPathSubjectPart(TypedPathFormatter.Media.ProductModelNameFormatter), new ParentPathSubjectPart(),
//                new TypedPathSubjectPart(TypedPathFormatter.Media.ProductNumberFormatter)
//            ]
//        ]
//
//        public void Process(IRootContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
//        [
//            var parts = new PathSubjectPart[] [ new ParentPathSubjectPart(), new ConstantPathSubjectPart("Media"), new ParentPathSubjectPart() ]
//               .Concat(match)
//               .Concat(rest)
//               .ToArray()
//            var path = new AbsolutePathSubject(parts)
//            context.Converter.Convert(path, scope, output)
//        ]
//    ]
//]
