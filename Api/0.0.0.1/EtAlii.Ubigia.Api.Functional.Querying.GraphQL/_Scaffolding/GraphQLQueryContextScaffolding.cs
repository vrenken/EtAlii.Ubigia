namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Querying.GraphQL;
    using EtAlii.xTechnology.MicroContainer;
    using GraphQL;
    using GraphQL.Execution;
    using GraphQL.Validation;
    using GraphQL.Validation.Complexity;

    //using GraphQL.Http;

    internal class GraphQLQueryContextScaffolding : IScaffolding
    {
        private readonly IDataContext _dataContext;

        public GraphQLQueryContextScaffolding(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void Register(Container container)
        {
            container.Register<IGraphQLQueryContext, GraphQLQueryContext>();

            container.Register<IDataContext>(() => _dataContext);
            container.Register<IScriptsSet>(() => _dataContext.Scripts);

            
            container.Register<IDependencyResolver>(() => new FuncDependencyResolver(type =>
            {
                if (type == typeof(PersonType))
                {
                    var data = container.GetInstance<IUbigiaData>();
                    return new PersonType(data);
                }
                if (type == typeof(HumanType))
                {
                    var data = container.GetInstance<IUbigiaData>();
                    return new HumanType(data);
                }
                if (type == typeof(HumanInputType))
                {
                    return new HumanInputType();
                }
                if (type == typeof(DroidType))
                {
                    var data = container.GetInstance<IUbigiaData>();
                    return new DroidType(data);
                }
                if (type == typeof(CharacterInterface))
                {
                    return new CharacterInterface();
                }
                if (type == typeof(EpisodeEnum))
                {
                    return new EpisodeEnum();
                }
                return container.GetInstance(type);
            }));

            container.Register<IDocumentValidator, DocumentValidator>();
            container.Register<IDocumentBuilder, GraphQLDocumentBuilder>();
            container.Register<IComplexityAnalyzer>(() => new ComplexityAnalyzer());

            container.Register<IDocumentExecuter>(() =>
            {
                var documentBuilder = container.GetInstance<IDocumentBuilder>();
                var documentValidator = container.GetInstance<IDocumentValidator>();
                var complexityAnalyzer = container.GetInstance<IComplexityAnalyzer>();
                return new DocumentExecuter(documentBuilder, documentValidator, complexityAnalyzer);
            });
            //container.Register<IDocumentWriter, DocumentWriter>();
            
            container.Register<IUbigiaData, UbigiaData>();
            container.Register<IStaticQuery, StaticQuery>();
            container.Register<IStaticMutation, StaticMutation>();
            container.Register<IStaticSchema, StaticSchema>();

            //container.Register<IHttpContextAccessor, HttpContextAccessor>();

        }
    }
}