namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.Servus.Api.Logical;
    using Remotion.Linq.Clauses;

    public interface IScriptAggregator
    {
        void AddAddItem(string path);
        void AddVariableAssignment(NodeQueryable<INode> nodeQuery);
        void AddVariableAssignment(IQuerySource querySource);
        string GetScript();
        void AddUpdateItem(Identifier identifier, string updateVariableName);
        void AddGetItem(Identifier identifier);
    }
}