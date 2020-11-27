namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.Ubigia.Api.Logical;
    using Remotion.Linq.Clauses;

    public interface IScriptAggregator
    {
        void AddAddItem(string path);
        void AddVariableAssignment(NodeQueryable<INode> nodeQuery);
        void AddVariableAssignment(IQuerySource querySource);
        string GetScript();
        void AddUpdateItem(in Identifier identifier, string updateVariableName);
        void AddGetItem(in Identifier identifier);
    }
}