// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
	using System.Collections.Generic;
	using System.IO;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;

	public class ConfigurationDetailsParser
	{
		private const string FolderPattern = @"(?<={{FOLDER:)(\w+?)@([0-9A-Za-z \\%]+?)(?=}})";
		private const string PortPattern = @"(?<={{PORT:)(\w+?)@(\d+?)(?=}})";
		private const string PathPattern = @"(?<={{PATH:)(\w+?)@([0-9A-Za-z\/]*?)(?=}})";
		private const string HostPattern = @"(?<={{HOST:)(\w+?)@([0-9A-Za-z.]+?)(?=}})";

	    public async Task<ConfigurationDetails> Parse(string configurationFile, bool replace = true)
	    {
		    var configuration = await File
                .ReadAllTextAsync(configurationFile)
                .ConfigureAwait(false);

		    // Folder matching.
		    var folderMatches = Regex.Matches(configuration, FolderPattern);
		    var folders = new Dictionary<string, string>();

		    foreach (Match match in folderMatches)
		    {
			    var name = match.Groups[1].Value;
			    var folder = match.Groups[2].Value;
			    folders.Add(name, folder.Replace("\\\\","\\"));

			    if (replace)
			    {
				    configuration = configuration.Replace($"{{{{FOLDER:{name}@{folder}}}}}", folder);
			    }
		    }

		    // Host matching.
		    var hostMatches = Regex.Matches(configuration, HostPattern);
		    var hosts = new Dictionary<string, string>();

		    foreach (Match match in hostMatches)
		    {
			    var name = match.Groups[1].Value;
			    var host = match.Groups[2].Value;
			    hosts.Add(name, host);

			    if (replace)
			    {
				    configuration = configuration.Replace($"{{{{HOST:{name}@{host}}}}}", host);
			    }
		    }

		    // Port matching.
		    var portMatches = Regex.Matches(configuration, PortPattern);
		    var ports = new Dictionary<string, int>();

		    foreach (Match match in portMatches)
		    {
			    var name = match.Groups[1].Value;
			    var port = int.Parse(match.Groups[2].Value);
			    ports.Add(name, port);

			    if (replace)
			    {
				    configuration = configuration.Replace($"{{{{PORT:{name}@{port}}}}}", $"{port}");

			    }
		    }

		    // Path matching.
		    var pathMatches = Regex.Matches(configuration, PathPattern);
		    var paths = new Dictionary<string, string>();

		    foreach (Match match in pathMatches)
		    {
			    var name = match.Groups[1].Value;
			    var path = match.Groups[2].Value;
			    paths.Add(name, path);

			    if (replace)
			    {
				    configuration = configuration.Replace($"{{{{PATH:{name}@{path}}}}}", path);
			    }
		    }

		    return new ConfigurationDetails(folders, hosts, ports, paths, configuration);
	    }
    }
}
