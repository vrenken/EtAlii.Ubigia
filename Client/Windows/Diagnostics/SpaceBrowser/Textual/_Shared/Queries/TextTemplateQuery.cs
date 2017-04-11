
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;

    public class TextTemplateQuery : QueryBase<ITextTemplateQueryHandler, string>
    {
        public string Name { get; }

        public TextTemplateQuery(string name)
        {
            Name = name;
        }
    }
}
