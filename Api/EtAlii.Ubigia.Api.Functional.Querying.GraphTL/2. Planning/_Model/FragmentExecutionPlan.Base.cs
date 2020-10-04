namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    public abstract class FragmentExecutionPlan
    {
        public abstract Type OutputType { get; }

        internal FragmentMetadata Metadata { get; private set; }

        internal abstract Task<IObservable<Structure>> Execute(SchemaExecutionScope executionScope);
        
        internal void SetMetaData(FragmentMetadata metadata)
        {
            Metadata = metadata;
        }
    }
}