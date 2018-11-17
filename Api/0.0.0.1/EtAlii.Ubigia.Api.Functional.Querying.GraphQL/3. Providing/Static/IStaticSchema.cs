namespace EtAlii.Ubigia.Api.Functional
{
    using GraphQL;
    using GraphQL.Types;

    internal interface IStaticSchema : ISchema
    {
        IDependencyResolver DependencyResolver { get; }
    }
}