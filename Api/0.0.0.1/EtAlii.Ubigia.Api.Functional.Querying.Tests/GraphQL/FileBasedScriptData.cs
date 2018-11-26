using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Collections;
    using System.Threading.Tasks;
    using GraphQL;
    using GraphQL.Http;
    using Newtonsoft.Json.Linq;
    using Xunit;


    public class FileBasedScriptData : TheoryData<string, string, string>
    {
        public FileBasedScriptData()
        {
            var folder = Directory.GetCurrentDirectory();
            folder = Path.Combine(folder, "GraphQL");
            var files = Directory.GetFiles(folder);

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var fileContent = File.ReadAllText(file);

                var fragments = fileContent.Split("###");
                foreach (var fragment in fragments)
                {
                    var lines = fragment.Replace("\r\n", "\n").Split("\n");
                    if (lines.Length > 1)
                    {
                        var title = lines[0];
                        var query = String.Join("\n", lines.Skip(1).ToArray());
                        Add(fileName, title, query);
                    }
                }
            }
        }
    }
}
