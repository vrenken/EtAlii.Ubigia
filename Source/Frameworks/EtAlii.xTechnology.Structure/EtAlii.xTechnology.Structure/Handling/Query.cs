namespace EtAlii.xTechnology.Structure
{
    public class Query<TParam> : IQuery<TParam>
    {
        TParam IParams<TParam>.Parameter { get { return _parameter; } }
        private readonly TParam _parameter;

        public Query(TParam parameter)
        {
            _parameter = parameter;
        }
    }

}
