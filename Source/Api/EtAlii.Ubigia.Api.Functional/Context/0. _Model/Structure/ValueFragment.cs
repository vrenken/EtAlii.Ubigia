// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    public class ValueFragment : Fragment
    {
        public MutationValue Mutation { get; }
        public ValuePrefix Prefix { get; }
        public ValueAnnotation Annotation { get; }

        public ValueFragment(
            string name,
            ValuePrefix valuePrefix,
            ValueAnnotation annotation,
            FragmentType fragmentType,
            MutationValue mutation)
            : base(name, fragmentType)
        {
            Prefix = valuePrefix;
            Annotation = annotation;
            Mutation = mutation;
        }

        public override string ToString()
        {
            var prefixAsString = Prefix != null
                ? $"{Prefix} "
                : string.Empty;
            var annotationAsString = Annotation != null
                ? $" {Annotation}"
                : string.Empty;
            return $"{prefixAsString}{Name}{annotationAsString}";
        }
    }
}
