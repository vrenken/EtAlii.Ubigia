
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;

    public class TextTemplateQuery : QueryBase<TextTemplateQueryHandler, string>
    {
        public string Name { get; private set; }

        public TextTemplateQuery(string name)
        {
            Name = name;
        }
    }
}
