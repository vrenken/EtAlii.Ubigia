
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
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
