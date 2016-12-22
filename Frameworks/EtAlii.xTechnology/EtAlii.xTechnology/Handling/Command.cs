namespace EtAlii.xTechnology.Structure
{
    public class Command<TParam> : ICommand<TParam>
    {
        TParam IParams<TParam>.Parameter { get { return _parameter; } }
        private readonly TParam _parameter;

        public Command(TParam parameter)
        {
            _parameter = parameter;
        }
    }

}
