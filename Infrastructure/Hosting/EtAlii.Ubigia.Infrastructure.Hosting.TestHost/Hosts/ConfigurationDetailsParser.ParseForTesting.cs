namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Threading.Tasks;
	using EtAlii.xTechnology.Hosting;
	using EtAlii.xTechnology.Networking;

	public static class ConfigurationDetailsParserParseForTestingExtensions
	{
		/// <summary>
		///  This is a temporary class to make sure we can test and develop the new FreePortFinder with all the unit tests available in this solution.
		/// Afterwards this extension should be replaced by the original and the
		/// protected override async Task&lt;ConfigurationDetails&gt; ParseForTesting(string configurationFile)
		/// Should be removed.
		/// </summary>
		/// <param name="parser"></param>
		/// <param name="configurationFile"></param>
		/// <returns></returns>
		public static async Task<ConfigurationDetails> ParseForTestingWithFreePortFindingChanges(this ConfigurationDetailsParser parser, string configurationFile)
		{
			var details = await parser.Parse(configurationFile, false);
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

			var neededPorts = (ushort)details.Ports.Count;
			var freePorts = Ipv4FreePortFinder.Current.Get(12000, neededPorts);
			
			var testPorts = new Dictionary<string, int>();

			ushort i = 0;
			foreach (var (name, originalPort) in details.Ports)
			{
				var testPort = freePorts[i++];
				testPorts.Add(name, testPort);

				configuration = configuration.Replace($"{{{{PORT:{name}@{originalPort}}}}}", $"{testPort}");
			}


			// We just take the default value for all of the path variables
			foreach (var (name, originalPath) in details.Paths)
			{
				configuration = configuration.Replace($"{{{{PATH:{name}@{originalPath}}}}}", originalPath);
			}

			return new ConfigurationDetails(testFolders, details.Hosts, testPorts, details.Paths, configuration);
		}
	}
}