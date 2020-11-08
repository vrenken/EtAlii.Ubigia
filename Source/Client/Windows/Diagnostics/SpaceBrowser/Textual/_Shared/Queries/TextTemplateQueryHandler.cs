
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using EtAlii.xTechnology.Structure.Workflow;

    public class TextTemplateQueryHandler : QueryHandlerBase<TextTemplateQuery, string>, ITextTemplateQueryHandler
    {
        protected override IQueryable<string> Handle(TextTemplateQuery query)
        {
            var result = new List<string>();

            // Get the current executing assembly (in this case it's the test dll)
            var assembly = Assembly.GetExecutingAssembly();
            
            Stream stream = null;
            try
            {
                // Get the stream (embedded resource) - be sure to wrap in a using block
                stream = assembly.GetManifestResourceStream(query.Name);
                using (var reader = new StreamReader(stream ?? throw new InvalidOperationException($"No manifest resource stream found: {query.Name ?? "NULL"}")))
                {
                    stream = null;
                    var template = reader.ReadToEnd();
                    result.Add(template);
                }
            }
            finally
            {
                stream?.Dispose();
            }
            
            return result.AsQueryable();
        }
    }
}
