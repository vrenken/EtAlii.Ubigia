
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;
    using System.Linq;
    using System.Collections.Generic;
    using System.Reflection;
    using System.IO;

    public class TextTemplateQueryHandler : QueryHandlerBase<TextTemplateQuery, string>, ITextTemplateQueryHandler
    {
        public TextTemplateQueryHandler()  
        {
        }

        protected override IQueryable<string> Handle(TextTemplateQuery query)
        {
            var result = new List<string>();

            // Get the current executing assembly (in this case it's the test dll)
            var assembly = Assembly.GetExecutingAssembly();
            // Get the stream (embedded resource) - be sure to wrap in a using block
            using (var stream = assembly.GetManifestResourceStream(query.Name))
            {
                using (var reader = new StreamReader(stream))
                {
                    var template = reader.ReadToEnd();
                    result.Add(template);
                }
            }
            return result.AsQueryable();
        }
    }
}
