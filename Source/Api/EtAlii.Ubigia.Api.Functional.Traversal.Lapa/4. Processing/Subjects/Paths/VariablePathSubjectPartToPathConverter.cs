namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using Moppet.Lapa;

    internal class VariablePathSubjectPartToPathConverter : IVariablePathSubjectPartToPathConverter
    {
        private readonly INonRootedPathSubjectParser _nonRootedPathSubjectParser;
        private readonly LpsParser _nonRootedParser;
        private readonly INodeValidator _nodeValidator;
        private const string _id = "VariablePathSubjectPartToPathConverter";
        private readonly IScriptValidator _scriptValidator;

        public VariablePathSubjectPartToPathConverter(
            INonRootedPathSubjectParser nonRootedPathSubjectParser,
            INodeValidator nodeValidator,
            IScriptValidator scriptValidator)
        {
            _nonRootedPathSubjectParser = nonRootedPathSubjectParser;
            _nodeValidator = nodeValidator;
            _nonRootedParser = new LpsParser(_id, true, _nonRootedPathSubjectParser.Parser);
            _scriptValidator = scriptValidator;
        }

        public async Task<PathSubjectPart[]> Convert(ScopeVariable variable)
        {
            // TODO: At this moment we only allow single items to be used as path variables.
            var value = await variable.Value.SingleAsync();

            return value switch
            {
                string s => ToPath(s, variable.Source),
                INode node => ToPath(node),
                NonRootedPathSubject nonRootedPathSubject => nonRootedPathSubject.Parts,
                StringConstantSubject stringConstantSubject => ToPath(stringConstantSubject),
                RootedPathSubject rootedPathSubject => ToPath(rootedPathSubject),
                _ => throw new InvalidOperationException($"Unable to select option for criteria: {(value != null ? value.ToString() : "[NULL]")}")
            };
        }

        private PathSubjectPart[] ToPath(RootedPathSubject rootedPathSubject)
        {
            return Array.Empty<PathSubjectPart>()
                .Concat(new PathSubjectPart[]
                {
                    new ParentPathSubjectPart(),
                    new ConstantPathSubjectPart(rootedPathSubject.Root),
                    new ParentPathSubjectPart(),
                })
                .Concat(rootedPathSubject.Parts)
                .ToArray();
        }

        private PathSubjectPart[] ToPath(string value, string variableName)
        {
            // TODO: This class should also be able to cope with rooted paths.
            var node = _nonRootedParser.Do(value);
            _nodeValidator.EnsureSuccess(node, _id, false);
            var childNode = node.Children.Single();

            return ParsePath(value, variableName, childNode);
        }

        private PathSubjectPart[] ToPath(INode node)
        {
            return new PathSubjectPart[] { new ParentPathSubjectPart(), new IdentifierPathSubjectPart(node.Id) };
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

            // There is a possibility that we receive a string constant that needs to be validated.
            _scriptValidator.Validate(pathSubject);

            return pathSubject switch
            {
                NonRootedPathSubject nonRootedPathSubject => nonRootedPathSubject.Parts,
                StringConstantSubject stringConstantSubject => ToPath(stringConstantSubject),
                RootedPathSubject rootedPathSubject => ToPath(rootedPathSubject),
                _ => throw new InvalidOperationException($"Unable to select option for criteria: {value ?? "[NULL]"}")
            };
        }
    }
}
