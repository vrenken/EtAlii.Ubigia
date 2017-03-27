namespace EtAlii.xTechnology.UnitTests
{
    using EtAlii.xTechnology.Structure;

    internal class IntegerToArrayQuery : Query<int>
    {
        public IntegerToArrayQuery(int parameter) 
            : base(parameter)
        {
        }

        public int Integer => ((IParams<int>)this).Parameter;
    }
}