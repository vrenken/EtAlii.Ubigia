// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure
{
    public class Command<TParam> : ICommand<TParam>
    {
        TParam IParams<TParam>.Parameter => _parameter;
        private readonly TParam _parameter;

        public Command(TParam parameter)
        {
            _parameter = parameter;
        }
    }

    //public class Command<TParam1, TParam2> : ICommand<TParam1, TParam2>
    //[
    //    TParam1 IParams<TParam1, TParam2>.Parameter1 [ get [ return _parameter1 ] ]
    //    private readonly TParam1 _parameter1

    //    TParam2 IParams<TParam1, TParam2>.Parameter2 [ get [ return _parameter2 ] ]
    //    private readonly TParam2 _parameter2

    //    public Command(TParam1 parameter1, TParam2 parameter2)
    //    [
    //        _parameter1 = parameter1
    //        _parameter2 = parameter2
    //    ]
    //]

    //public class Command<TParam1, TParam2, TParam3> : ICommand<TParam1, TParam2, TParam3>
    //[
    //    TParam1 IParams<TParam1, TParam2, TParam3>.Parameter1 [ get [ return _parameter1 ] ]
    //    private readonly TParam1 _parameter1

    //    TParam2 IParams<TParam1, TParam2, TParam3>.Parameter2 [ get [ return _parameter2 ] ]
    //    private readonly TParam2 _parameter2

    //    TParam3 IParams<TParam1, TParam2, TParam3>.Parameter3 [ get [ return _parameter3 ] ]
    //    private readonly TParam3 _parameter3

    //    public Command(TParam1 parameter1, TParam2 parameter2, TParam3 parameter3)
    //    [
    //        _parameter1 = parameter1
    //        _parameter2 = parameter2
    //        _parameter3 = parameter3
    //    ]
    //]
}
