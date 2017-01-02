namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Collections.Generic;
    using System.Linq;

    internal class ActionsParser
    {
        private readonly PathParser _pathParser;
        private readonly VariableParser _variableParser;
        private readonly IParserHelper _parserHelper;

        private readonly IEnumerable<IActionParser> _actionParsers;

        public LpsParser ExpressionParser { get { return _expressionParser; } }
        private readonly LpsParser _expressionParser;

        public ActionsParser(
            TerminalExpressions terminalExpressions,
            PathExpressions pathExpressions,
            IParserHelper parserHelper,
            PathParser pathParser, 
            VariableParser variableParser,
            AddItemActionParser addItemActionParser,
            RemoveItemActionParser removeItemActionParser,
            UpdateItemActionParser updateItemActionParser,
            ItemOutputActionParser itemOutputActionParser,
            ItemsOutputActionParser itemsOutputActionParser,
            VariableAddItemActionParser variableAddItemActionParser,
            VariableUpdateActionParser variableUpdateItemActionParser,
            VariableItemAssignmentActionParser variableItemAssignmentActionParser, 
            VariableItemsAssignmentActionParser variableItemsAssignmentActionParser,
            VariableStringAssignmentActionParser variableStringAssignmentActionParser,
            VariableOutputActionParser variableOutputActionParser)
        {
            _parserHelper = parserHelper;
            _pathParser = pathParser;
            _variableParser = variableParser;

            _actionParsers = new IActionParser[]
            {
                updateItemActionParser,
                variableUpdateItemActionParser,
                addItemActionParser,
                removeItemActionParser,
                itemsOutputActionParser,
                itemOutputActionParser,
                variableAddItemActionParser,
                variableItemsAssignmentActionParser,
                variableItemAssignmentActionParser,
                variableStringAssignmentActionParser,
                variableOutputActionParser
            };

            var expressions = new LpsAlternatives();
            foreach (var actionParser in _actionParsers)
            {
                expressions |= (actionParser.ExpressionParser + terminalExpressions.Comment);
            }
            expressions |= terminalExpressions.Comment;
            _expressionParser = 
                (expressions + terminalExpressions.ActionEnd.OneOrMore()).ZeroOrMore() +
                (expressions + terminalExpressions.ActionEnd.ZeroOrMore()).Maybe();

        }
        
        public IEnumerable<Action> Parse(IEnumerable<LpNode> nodes)
        {
            var actions = new List<Action>();

            foreach (var node in nodes)
            {
                _parserHelper.EnsureSuccess(node, "action", false);

                var actionNodes = FindActionNodes(node);
                foreach(var actionNode in actionNodes)
                {
                    var action = Parse(actionNode);
                    actions.Add(action);
                }
            }

            return actions;
        }

        private IEnumerable<LpNode> FindActionNodes(LpNode node)
        {
            var result = new List<LpNode>();
            if(node.Id != null && node.Match != null)
            {
                result.Add(node);
            }
            else if(node.Children != null)
            {
                foreach(var childNode in node.Children)
                {
                    var nodes = FindActionNodes(childNode);
                    result.AddRange(nodes);
                }
            }
            return result;
        }

        public Action Parse(LpNode node)
        {
            var parser = _actionParsers.First(p => p.ActionId == node.Id);
            var result = parser.Parse(node);
            return result;
        }
    }
}
