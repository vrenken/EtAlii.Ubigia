namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Structure;

    public class ResultFactory : IResultFactory
    {
        private readonly ISelector<object, Func<object, object,Result>> _converterSelector;

        public ResultFactory()
        {
            _converterSelector = new Selector<object, Func<object, object, Result>>()
                .Register(o => o is IInternalNode, (o, g) => ToResult((IInternalNode)o, g))
                .Register(o => o is INode, (o, g) => ToResult((IInternalNode)o, g))
                .Register(o => o is Identifier, (o, g) => ToResult((Identifier)o, g))
                .Register(o => o is int, (o, g) => ToResult((int)o, g))
                .Register(o => o is string, (o, g) => ToResult((string)o, g))
                .Register(o => o is DateTime, (o, g) => ToResult((DateTime)o, g))
                .Register(o => o == null, (o, g) => new Result(null, "None", new PropertyDictionary { ["Value"] = o }, g))
                .Register(o => true, (o, g) => new Result(null, o.GetType().Name, new PropertyDictionary {["Value"] = o }, g));
        }

        public Result Convert(object o, object group)
        {
            var converter = _converterSelector.Select(o);
            return converter(o, group);
        }
        private Result ToResult(int o, object group)
        {
            return new Result(null, "Integer", new PropertyDictionary {["Value"] = o }, group);
        }

        private Result ToResult(string o, object group)
        {
            return new Result(null, "String", new PropertyDictionary {["Value"] = o }, group);
        }

        private Result ToResult(DateTime o, object group)
        {
            return new Result(null, "DateTime", new PropertyDictionary {["Value"] = o }, group);
        }

        private Result ToResult(IInternalNode node, object group)
        {
            return new Result(node.Entry.Id.ToTimeString(), node.Entry.Type, node.GetProperties(), group);
        }

        private Result ToResult(Identifier identifier, object group)
        {
            var properties = new PropertyDictionary
            {
                ["Storage"] = identifier.Storage,
                ["Space"] = identifier.Space,
                ["Account"] = identifier.Account,
                ["Era"] = identifier.Era,
                ["Period"] = identifier.Period,
                ["Moment"] = identifier.Moment,
            };

            return new Result(identifier.ToTimeString(), "Identifier", properties, group);
        }
    }
}
