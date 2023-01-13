// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/// <summary>
/// This class can be used to acquire more details about the application configuration (i.e. appsettings.json & co.), and to inject new values into the application settings.
/// It should not be used to acquire settings from the application configuration, as was the case in the past.
/// As the Microsoft.Extensions.Configuration functionalities have gotten quite mature in the past few years we should also reconsider the approach below and maybe find a way to fetch and
/// modify values using standard IConfiguration methodologies. All in all the setup is under the hood 'just' a fancy key-value store.
/// </summary>
public class ConfigurationDetailsParser
{
    private const string FolderPattern = @"(?<={{FOLDER:)(\w+?)@([0-9A-Za-z \\%]+?)(?=}})";
    private const string PortPattern = @"(?<={{PORT:)(\w+?)@(\d+?)(?=}})";
    private const string PathPattern = @"(?<={{PATH:)(\w+?)@([0-9A-Za-z\/]*?)(?=}})";
    private const string HostPattern = @"(?<={{HOST:)(\w+?)@([0-9A-Za-z.]+?)(?=}})";

    /// <summary>
    /// Parse the provided configuration file and extract replaceable values like folders, ports, paths and hosts.
    /// </summary>
    /// <param name="configurationFile"></param>
    /// <param name="replace"></param>
    /// <returns></returns>
    public async Task<ConfigurationDetails> Parse(string configurationFile, bool replace = true)
    {
        var configuration = await File
            .ReadAllTextAsync(configurationFile)
            .ConfigureAwait(false);

        // Folder matching.
        var folderMatchGroups = Regex
            .Matches(configuration, FolderPattern)
            .Select(match => match.Groups)
            .ToArray();
        var folders = new Dictionary<string, string>();

        foreach (var matchGroups in folderMatchGroups)
        {
            var name = matchGroups[1].Value;
            var folder = matchGroups[2].Value;
            folders.Add(name, folder.Replace("\\\\","\\"));

            if (replace)
            {
                configuration = configuration.Replace($"{{{{FOLDER:{name}@{folder}}}}}", folder);
            }
        }

        // Host matching.
        var hostMatchGroups = Regex
            .Matches(configuration, HostPattern)
            .Select(match => match.Groups)
            .ToArray();
        var hosts = new Dictionary<string, string>();

        foreach (var matchGroups in hostMatchGroups)
        {
            var name = matchGroups[1].Value;
            var host = matchGroups[2].Value;
            hosts.Add(name, host);

            if (replace)
            {
                configuration = configuration.Replace($"{{{{HOST:{name}@{host}}}}}", host);
            }
        }

        // Port matching.
        var portMatchGroups = Regex
            .Matches(configuration, PortPattern)
            .Select(match => match.Groups)
            .ToArray();

        var ports = new Dictionary<string, int>();

        foreach (var matchGroups in portMatchGroups)
        {
            var name = matchGroups[1].Value;
            var port = int.Parse(matchGroups[2].Value);
            ports.Add(name, port);

            if (replace)
            {
                configuration = configuration.Replace($"{{{{PORT:{name}@{port}}}}}", $"{port}");

            }
        }

        // Path matching.
        var pathMatchGroups = Regex
            .Matches(configuration, PathPattern)
            .Select(match => match.Groups)
            .ToArray();
        var paths = new Dictionary<string, string>();

        foreach (var matchGroups in pathMatchGroups)
        {
            var name = matchGroups[1].Value;
            var path = matchGroups[2].Value;
            paths.Add(name, path);

            if (replace)
            {
                configuration = configuration.Replace($"{{{{PATH:{name}@{path}}}}}", path);
            }
        }

        return new ConfigurationDetails(folders, hosts, ports, paths, configuration);
    }
}
