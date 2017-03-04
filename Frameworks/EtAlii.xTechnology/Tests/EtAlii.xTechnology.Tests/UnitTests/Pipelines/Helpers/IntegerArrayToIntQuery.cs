namespace EtAlii.xTechnology.Tests
{
    using EtAlii.xTechnology.Structure;

    public class IntegerArrayToIntQuery : Query<int[]>
    {
        public IntegerArrayToIntQuery(int[] parameter) 
            : base(parameter)
        {
        }

        public int[] Array => ((IParams<int[]>)this).Parameter;
    }
}