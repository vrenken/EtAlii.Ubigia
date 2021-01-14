// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Context.Antlr;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public partial class ContextVisitor
    {
        public override object VisitValue_annotation_assign_and_select_with_key(ContextSchemaParser.Value_annotation_assign_and_select_with_keyContext context)
        {
            var sourcePath = (PathSubject)VisitSchema_path(context.schema_path());
            var name = (string)VisitSchema_key(context.schema_key());
            return new AssignAndSelectValueAnnotation(sourcePath, new StringConstantSubject(name));
        }

        public override object VisitValue_annotation_assign_and_select_without_key(ContextSchemaParser.Value_annotation_assign_and_select_without_keyContext context)
        {
            var name = (string)VisitSchema_key(context.schema_key());
            return new AssignAndSelectValueAnnotation(null, new StringConstantSubject(name));
        }

        public override object VisitValue_annotation_clear_and_select_with_key(ContextSchemaParser.Value_annotation_clear_and_select_with_keyContext context)
        {
            var subject = (Subject)VisitSchema_path(context.schema_path());

            var sourcePath = subject switch
            {
                PathSubject pathSubject => pathSubject,
                StringConstantSubject stringConstantSubject => new RelativePathSubject(new ConstantPathSubjectPart(stringConstantSubject.Value)),
                _ => throw new NotSupportedException($"Cannot convert path subject in: {subject}")
            };

            return new ClearAndSelectValueAnnotation(sourcePath);
        }

        public override object VisitValue_annotation_clear_and_select_without_key(ContextSchemaParser.Value_annotation_clear_and_select_without_keyContext context)
        {
            return new ClearAndSelectValueAnnotation(null);
        }

        public override object VisitValue_annotation_select(ContextSchemaParser.Value_annotation_selectContext context)
        {
            var pathContext = context.schema_path();
            var sourcePath = pathContext != null
                ? (PathSubject)VisitSchema_path(context.schema_path())
                : null;
            return new SelectValueAnnotation(sourcePath);
        }
    }
}
