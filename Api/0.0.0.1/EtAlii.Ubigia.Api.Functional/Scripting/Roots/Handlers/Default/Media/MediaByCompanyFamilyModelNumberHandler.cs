namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    internal class MediaByCompanyFamilyModelNumberHandler : IRootHandler
    {
        public PathSubjectPart[] Template { get; }

        public MediaByCompanyFamilyModelNumberHandler()
        {
            // media:COMPANY/FAMILY/MODEL/NUMBER
            Template = new PathSubjectPart[]
            {
                new TypedPathSubjectPart(TypedPathFormatter.Media.CompanyNameFormatter), new IsParentOfPathSubjectPart(),
                new TypedPathSubjectPart(TypedPathFormatter.Media.ProductFamilyNameFormatter), new IsParentOfPathSubjectPart(),
                new TypedPathSubjectPart(TypedPathFormatter.Media.ProductModelNameFormatter), new IsParentOfPathSubjectPart(),
                new TypedPathSubjectPart(TypedPathFormatter.Media.ProductNumberFormatter)
            };
        }

        public void Process(IRootContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var parts = new PathSubjectPart[] { new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Media"), new IsParentOfPathSubjectPart() }
               .Concat(match)
               .Concat(rest)
               .ToArray();
            var path = new AbsolutePathSubject(parts);
            context.Converter.Convert(path, scope, output);
        }
    }
}