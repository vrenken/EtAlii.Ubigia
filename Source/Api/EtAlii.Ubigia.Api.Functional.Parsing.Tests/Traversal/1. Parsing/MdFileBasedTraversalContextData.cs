// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests;

using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

public class MdFileBasedTraversalScriptData : TheoryData<string, string, string>
{
    private string FindSolutionFolder(string currentPath)
    {
        bool hasSolutionFile;
        do
        {
            currentPath = Path.Combine(currentPath, "..");
            hasSolutionFile = Directory.GetFiles(currentPath, "*.sln").Any();
        } while (!hasSolutionFile);

        return currentPath;
    }

    public MdFileBasedTraversalScriptData()
    {
        var folder = Directory.GetCurrentDirectory();
        var solutionFolder = FindSolutionFolder(folder);
        var documentsFolder = Path.Combine(solutionFolder, "..", "Documentation");

        var options = new EnumerationOptions { RecurseSubdirectories = true };
        var files = Directory.GetFiles(documentsFolder, "*.md", options);

        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);
            var fileContent = File.ReadAllText(file);

            var results = Regex.Matches(fileContent, "```gtl(.*?)```", RegexOptions.Singleline);
            foreach (Match match in results)
            {
                var fragment = match.Groups[1].Value;
                var position = match.Groups[1].Index;
                var lineNumber = fileContent.Take(position).Count(c => c == '\n') + 2;

                var lines = fragment
                    .TrimStart('\r', '\n')
                    .Replace("\r\n", "\n")
                    .Split("\n")
                    .ToArray();

                if (lines.Length > 0)
                {
                    var query = string.Join("\r\n", lines.ToArray());
                    Add(fileName, $"{lineNumber:0000}", query);
                }
            }
        }
    }
}
