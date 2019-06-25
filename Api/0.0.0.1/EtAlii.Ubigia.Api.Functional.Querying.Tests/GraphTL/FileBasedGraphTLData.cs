namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.IO;
    using System.Linq;
    using Xunit;

    public class FileBasedGraphTLData : TheoryData<string, string, string>
    {
        public FileBasedGraphTLData()
        {
            var folder = Directory.GetCurrentDirectory();
            folder = Path.Combine(folder, "GraphTL");
            var files = Directory.GetFiles(folder);
                //.Where(fileName => fileName.EndsWith("Samples 0. - Introduction.txt"));
                //.Where(fileName => !fileName.EndsWith("Samples 2. - Nodes.txt"));

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var fileContent = File.ReadAllText(file);

                var fragments = fileContent.Split("-- =======");
                foreach (var fragment in fragments)
                {
                    var lines = fragment
                        .TrimStart('=', '\r', '\n')
                        .Replace("\r\n", "\n")
                        .Split("\n")
                        //.SkipWhile(string.IsNullOrWhiteSpace)
                        .ToArray();
                    if (lines.Length > 1)
                    {
                        var title = lines[0].Replace("-- ", "");
                        //if (!title.StartsWith("1. Traverses to a node and returns this as a named element.")) continue;
                        
                        var query = string.Join("\n", lines.ToArray());
                        Add(fileName, title, query);
                    }
                }
            }
        }
    }
}
