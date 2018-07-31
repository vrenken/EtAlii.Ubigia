namespace EtAlii.Ubigia.Api.Functional
{
    public interface IDataContext
    {
        IQuerySet Queries { get; }
        IScriptsSet Scripts { get; }
        IDataContextConfiguration Configuration { get; }
    }
}