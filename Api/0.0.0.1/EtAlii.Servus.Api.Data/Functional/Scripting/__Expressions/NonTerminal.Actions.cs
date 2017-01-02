namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System;

    public static partial class NonTerminal
    {
        public static class Actions
        {
            public static LpsParser Action { get { return _action.Value; } }
            private static Lazy<LpsParser> _action = new Lazy<LpsParser>(() =>
            {
                return
                    (
                    AddAction |
                    RemoveAction |
                    ItemsOutputAction |
                    ItemOutputAction |
                    VariableItemAddAction |
                    VariableItemsAssignmentAction |
                    VariableItemAssignmentAction |
                    VariableStringAssignmentAction |
                    VariableOutputAction |
                    Terminal.Comment 
                    ) 
                    + Lp.ZeroOrMore(Terminal.ActionEnd);
            });

            public static LpsParser ItemOutputAction { get { return _itemOutputAction.Value; } }
            private static Lazy<LpsParser> _itemOutputAction = new Lazy<LpsParser>(() => (NonTerminal.Path).Id(NonTerminalId.Actions.ItemOutput) + Terminal.Comment);

            public static LpsParser ItemsOutputAction { get { return _itemsOutputAction.Value; } }
            private static Lazy<LpsParser> _itemsOutputAction = new Lazy<LpsParser>(() => (NonTerminal.Path).Id(NonTerminalId.Actions.ItemsOutput) + Terminal.Separator + Lp.Lookahead(Terminal.Separator.Not()) + Terminal.Comment);

            public static LpsParser VariableItemAssignmentAction { get { return _variableItemAssignmentAction.Value; } }
            private static Lazy<LpsParser> _variableItemAssignmentAction = new Lazy<LpsParser>(() => (Terminal.Variable + Terminal.Assignment + NonTerminal.Path).Id(NonTerminalId.Actions.VariableItemAssignment) + Terminal.Comment);

            public static LpsParser VariableItemsAssignmentAction { get { return _variableItemsAssignmentAction.Value; } }
            private static Lazy<LpsParser> _variableItemsAssignmentAction = new Lazy<LpsParser>(() => (Terminal.Variable + Terminal.Assignment + NonTerminal.Path).Id(NonTerminalId.Actions.VariableItemsAssignment) + Terminal.Separator + Terminal.Comment);

            public static LpsParser VariableStringAssignmentAction { get { return _variableStringAssignmentAction.Value; } }
            private static Lazy<LpsParser> _variableStringAssignmentAction = new Lazy<LpsParser>(() => (Terminal.Variable + Terminal.Assignment + Terminal.SymbolWithQuotes).Id(NonTerminalId.Actions.VariableStringAssignment) + Terminal.Comment);

            public static LpsParser VariableOutputAction { get { return _variableOutputAction.Value; } }
            private static Lazy<LpsParser> _variableOutputAction = new Lazy<LpsParser>(() => (Terminal.Variable).Id(NonTerminalId.Actions.VariableOutput));

            public static LpsParser AddAction { get { return _addAction.Value; } }
            private static Lazy<LpsParser> _addAction = new Lazy<LpsParser>(() => (NonTerminal.Path + Terminal.Separator + Terminal.AddStart + Terminal.Symbol).Id(NonTerminalId.Actions.Add) + Terminal.Comment);

            public static LpsParser VariableItemAddAction { get { return _variableItemAddAction.Value; } }
            private static Lazy<LpsParser> _variableItemAddAction = new Lazy<LpsParser>(() => (Terminal.Variable + Terminal.Assignment + NonTerminal.Path + Terminal.Separator + Terminal.AddStart + Terminal.Symbol).Id(NonTerminalId.Actions.VariableAdd) + Terminal.Comment);
            

            public static LpsParser RemoveAction { get { return _removeAction.Value; } }
            private static Lazy<LpsParser> _removeAction = new Lazy<LpsParser>(() => (NonTerminal.Path + Terminal.Separator + Terminal.RemoveStart + Terminal.Symbol).Id(NonTerminalId.Actions.Remove) + Terminal.Comment);
        }
    }
}
