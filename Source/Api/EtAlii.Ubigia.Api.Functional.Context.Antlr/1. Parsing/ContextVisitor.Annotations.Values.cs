// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Context.Antlr;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public partial class ContextVisitor
    {
        public override object VisitValue_annotation_assign_and_select_with_key(ContextSchemaParser.Value_annotation_assign_and_select_with_keyContext context)
        {
            var sourcePath = (PathSubject)VisitSchema_path(context.schema_path());
            var name = (string)VisitSchema_key(context.schema_key());
            // TODO: Wrong!
            return new AssignAndSelectValueAnnotation(sourcePath, sourcePath);
        }

        public override object VisitValue_annotation_assign_and_select_without_key(ContextSchemaParser.Value_annotation_assign_and_select_without_keyContext context)
        {
            var sourcePath = (PathSubject)VisitSchema_path(context.schema_path());
            return new AssignAndSelectValueAnnotation(sourcePath, null);
        }

        public override object VisitValue_annotation_clear_and_select_with_key(ContextSchemaParser.Value_annotation_clear_and_select_with_keyContext context)
        {
            var sourcePath = (PathSubject)VisitSchema_path(context.schema_path());
            var name = (string)VisitSchema_key(context.schema_key());
            // TODO: Wrong!
            return new ClearAndSelectValueAnnotation(sourcePath);
        }

        public override object VisitValue_annotation_clear_and_select_without_key(ContextSchemaParser.Value_annotation_clear_and_select_without_keyContext context)
        {
            var sourcePath = (PathSubject)VisitSchema_path(context.schema_path());
            // TODO: Wrong!
            return new ClearAndSelectValueAnnotation(sourcePath);
            // return new ClearAndSelectValueAnnotation(sourcePath, null);
        }
    }
}
