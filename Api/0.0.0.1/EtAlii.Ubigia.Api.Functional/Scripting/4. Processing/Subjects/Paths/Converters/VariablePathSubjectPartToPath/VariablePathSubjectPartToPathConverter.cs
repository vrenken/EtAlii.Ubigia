namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Reactive.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Structure;
    using Moppet.Lapa;

    class VariablePathSubjectPartToPathConverter : IVariablePathSubjectPartToPathConverter
    {
        private readonly ISelector<object, Func<object, string, PathSubjectPart[]>> _converterSelector;

        private readonly INonRootedPathSubjectParser _nonRootedPathSubjectParser;
        private readonly LpsParser _nonRootedParser;
        private readonly INodeValidator _nodeValidator;
        private const string Id = "VariablePathSubjectPartToPathConverter";
        private readonly ISelector<Subject, ISubjectParser> _parserSelector;

        public VariablePathSubjectPartToPathConverter(
            INonRootedPathSubjectParser nonRootedPathSubjectParser, 
            INodeValidator nodeValidator,
            IConstantSubjectsParser constantSubjectsParser)
        {
            _nonRootedPathSubjectParser = nonRootedPathSubjectParser;
            _nodeValidator = nodeValidator;
            _nonRootedParser = new LpsParser(Id, true, _nonRootedPathSubjectParser.Parser);

            _converterSelector = new Selector<object, Func<object, string, PathSubjectPart[]>>()
                .Register(variable => variable is string, (variable, variableName) => ToPath((string)variable, variableName))
                .Register(variable => variable is INode, (variable, variableName) => ToPath((INode)variable))
                .Register(variable => variable is NonRootedPathSubject, (variable, variableName) => ((NonRootedPathSubject)variable).Parts)
                .Register(variable => variable is StringConstantSubject, (variable, variableName) => ToPath((StringConstantSubject)variable))
                .Register(variable => variable is RootedPathSubject, (variable, variableName) => ToPath((RootedPathSubject)variable));

            _parserSelector = new Selector<Subject, ISubjectParser>()
                .Register(s => s is NonRootedPathSubject, nonRootedPathSubjectParser)
                //.Register(s => s is RootedPathSubject, rootedPathSubjectParser)
                .Register(s => s is ConstantSubject, constantSubjectsParser);
        }

        public PathSubjectPart[] Convert(ScopeVariable variable)
        {
            // TODO: At this moment we only allow single items to be used as path variables.
            var task = variable.Value
                .SingleAsync()
                .ToTask();
            task.Wait();
            var value = task.Result;
            var converter = _converterSelector.Select(value);
            return converter(value, variable.Source);
        }

        private PathSubjectPart[] ToPath(RootedPathSubject rootedPathSubject)
        {
            return new PathSubjectPart[0]
                .Concat(new PathSubjectPart[]
                {
                    new IsParentOfPathSubjectPart(),
                    new ConstantPathSubjectPart(rootedPathSubject.Root),
                    new IsParentOfPathSubjectPart(),
                })
                .Concat(rootedPathSubject.Parts)
                .ToArray();
        }

        private PathSubjectPart[] ToPath(string value, string variableName)
        {
            // TODO: This class should also be able to cope with rooted paths.
            var node = _nonRootedParser.Do(value);
            _nodeValidator.EnsureSuccess(node, Id, false);
            var childNode = node.Children.Single();

            return ParsePath(value, variableName, childNode);
        }

        private PathSubjectPart[] ToPath(INode node)
        {
            return new PathSubjectPart[] { new IsParentOfPathSubjectPart(), new IdentifierPathSubjectPart(node.Id) };
        }

        private PathSubjectPart[] ToPath(StringConstantSubject subject )
        {
            return new PathSubjectPart[] {new ConstantPathSubjectPart(subject.Value) };
        }

        private PathSubjectPart[] ParsePath(string value, string variableName, LpNode childNode)
        {
            if (!_nonRootedPathSubjectParser.CanParse(childNode))
            {
                throw new ScriptParserException($"Unable to parse variable for path (variable: {variableName}, value: {value})");
            }
            var pathSubject = _nonRootedPathSubjectParser.Parse(childNode);

            // There is a possibility that we receive a string constant that needs to be validated validation.
            var parser = _parserSelector.Select(pathSubject);
            if (!parser.CanValidate(pathSubject))
            {
                throw new ScriptParserException($"Unable to validate path in variable (variable: {variableName}, path: {pathSubject.ToString()})");
            }

            var converter = _converterSelector.Select(pathSubject);
            return converter(pathSubject, variableName);
        }

    }
}