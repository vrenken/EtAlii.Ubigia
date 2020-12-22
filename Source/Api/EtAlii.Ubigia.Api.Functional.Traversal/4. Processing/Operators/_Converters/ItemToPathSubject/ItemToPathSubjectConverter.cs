namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Structure;

    internal class ItemToPathSubjectConverter : IItemToPathSubjectConverter
    {
        private readonly ISelector<object, Func<object, PathSubject>> _converterSelector;

        public ItemToPathSubjectConverter()
        {
            _converterSelector = new Selector<object, Func<object, PathSubject>>()
                .Register(item => item is PathSubject, item => (PathSubject)item)
                .Register(item => item is INode, item => new RelativePathSubject(new ConstantPathSubjectPart(((INode)item).Type))) // << ????
                .Register(item => item is string, item => new RelativePathSubject(new ConstantPathSubjectPart((string) item)));
        }

        public PathSubject Convert(object items)
        {
            var converter = _converterSelector.Select(items);
            return converter(items);
        }

        public bool TryConvert(object items, out PathSubject pathSubject)
        {
            var converter = _converterSelector.TrySelect(items);
            if (converter != null)
            {
                pathSubject = converter(items);
                return true;
            }
            else
            {
                pathSubject = null;
                return false;
            }
        }
    }
}
