namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using System.IO;
    using System.Linq;
    using Xunit;

    public class FileBasedGraphQLData : TheoryData<string, string, string>
    {
        public FileBasedGraphQLData()
        {
            var folder = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(folder, "*Samples*.txt")
                .Where(fileName => !fileName.EndsWith("Samples 0. - Introduction.txt"))
                .Where(fileName => !fileName.EndsWith("Samples 2. - Mutations.txt"));

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
                        var query = string.Join("\n", lines.Skip(1).ToArray());
                        Add(fileName, title, query);
                    }
                }
            }
        }
    }
}
