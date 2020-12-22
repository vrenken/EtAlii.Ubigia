namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using System.IO;
    using System.Linq;
    using Xunit;

    public class FileBasedGraphContextData : TheoryData<string, string, string>
    {
        public FileBasedGraphContextData()
        {
            var folder = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(folder, "*Samples*.txt");
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
                        .ToArray();
                    if (lines.Length > 1)
                    {
                        var title = lines[0].Replace("-- ", "");
                        var query = string.Join("\n", lines.ToArray());
                        Add(fileName, title, query);
                    }
                }
            }
        }
    }
}
