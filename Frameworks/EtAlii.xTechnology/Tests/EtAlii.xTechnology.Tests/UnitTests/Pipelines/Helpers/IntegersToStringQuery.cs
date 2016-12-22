namespace EtAlii.xTechnology.Tests
{
    using EtAlii.xTechnology.Structure;

    public class IntegersToStringQuery : Query<int, int>
    {
        public IntegersToStringQuery(int parameter1, int parameter2) 
            : base(parameter1, parameter2)
        {
        }

        public int Integer1 { get { return ((IParams<int, int>)this).Parameter1; } }
        public int Integer2 { get { return ((IParams<int, int>)this).Parameter2; } }

    }
}