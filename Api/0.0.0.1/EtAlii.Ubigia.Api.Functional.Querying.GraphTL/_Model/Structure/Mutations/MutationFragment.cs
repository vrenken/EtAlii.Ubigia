namespace EtAlii.Ubigia.Api.Functional 
{
    public class MutationFragment : Fragment
    {
        public static readonly MutationFragment None = new EmptyMutationFragment();

        public MutationFragment(string name, Annotation annotation) : base(name, annotation)
        {
        }
        
        private class EmptyMutationFragment : MutationFragment
        {
            public EmptyMutationFragment() : base(nameof(None), Annotation.None)
            {
            }
        }

    }
}
