namespace EtAlii.xTechnology.Tests
{
    using EtAlii.xTechnology.Structure;

    public class IntegerArrayToStringQuery : Query<int[]>
    {
        public IntegerArrayToStringQuery(int[] parameter) 
            : base(parameter)
        {
        }

        public int[] Array => ((IParams<int[]>)this).Parameter;
    }
}