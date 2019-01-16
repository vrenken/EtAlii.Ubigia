namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Structure;

    public class MultiResultFactory : IMultiResultFactory
    {
        private readonly ISelector<object, Func<object, object,Result[]>> _converterSelector;

        private readonly IResultFactory _resultFactory;

        public MultiResultFactory(IResultFactory resultFactory)
        {
            _resultFactory = resultFactory;
            _converterSelector = new Selector<object, Func<object, object, Result[]>>()
                .Register(o => o is IPropertyDictionary, ConvertSingle)
                .Register(o => o is string, ConvertSingle)
                .Register(o => o is IEnumerable, ConvertMultiple)
                .Register(o => true, ConvertSingle);
        }

        private Result[] ConvertMultiple(object o, object group)
        {
            var result = new List<Result>();

            var enumerable = (IEnumerable) o;

            foreach (var e in enumerable)
            {
                var r = _resultFactory.Convert(e, group);
                result.Add(r);
            }

            return result.ToArray();
        }

        private Result[] ConvertSingle(object o, object group)
        {
            return new[]
            {
                _resultFactory.Convert(o, group)
            };
        }

        public Result[] Convert(object o, object group)
        {
            var converter = _converterSelector.Select(o);
            return converter(o, group);
        }
    }
}
