﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Threading.Tasks;

	public static class ConfigurationDetailsParserParseForTestingExtensions
    {
		public static async Task<ConfigurationDetails> ParseForTesting(this ConfigurationDetailsParser parser, string configurationFile)
        {
			var details = await parser
                .Parse(configurationFile, false)
                .ConfigureAwait(false);
			var configuration = details.Configuration;

			// We create a temporary folder for each of the folder variables
			var testFolders = new Dictionary<string, string>();
			foreach (var (name, originalFolder) in details.Folders)
			{
				var tempFolder = Path.Combine(Path.GetTempPath(), "EtAlii", Guid.NewGuid().ToString());
				configuration = configuration.Replace($"{{{{FOLDER:{name}@{originalFolder.Replace("\\","\\\\")}}}}}", tempFolder.Replace("\\","\\\\"));
				testFolders.Add(name, tempFolder);
			}

			// We just take the default value for all of the host variables
			foreach (var (name, originalHost) in details.Hosts)
			{
				configuration = configuration.Replace($"{{{{HOST:{name}@{originalHost}}}}}", originalHost);
			}

            // We had the free port finder her in the past. However it isn't needed anymore.
            foreach (var (name, originalPort) in details.Ports)
            {
                configuration = configuration.Replace($"{{{{PORT:{name}@{originalPort}}}}}", $"{originalPort}");
            }


			// We just take the default value for all of the path variables
			foreach (var (name, originalPath) in details.Paths)
			{
				configuration = configuration.Replace($"{{{{PATH:{name}@{originalPath}}}}}", originalPath);
			}

			return new ConfigurationDetails(testFolders, details.Hosts, details.Ports, details.Paths, configuration);
		}
	}
}
