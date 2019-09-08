﻿namespace EtAlii.Ubigia.Api.Functional 
{
    public class ValueFragment : Fragment
    {
        public object Mutation { get; }

        public ValueAnnotation Annotation { get; }

        internal ValueFragment(
            string name, 
            ValueAnnotation annotation, 
            Requirement requirement, 
            FragmentType fragmentType, 
            object mutation)
            : base(name, requirement, fragmentType)
        {
            Annotation = annotation;
            Mutation = mutation;
        }
                
        public override string ToString()
        {
            return $"{Name}{(Annotation != null ? " " + Annotation : string.Empty)}";
        }
    }
}
