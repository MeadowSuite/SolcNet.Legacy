using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.OSPlatform;
using static System.Runtime.InteropServices.Architecture;
using static System.Runtime.InteropServices.RuntimeInformation;
using PlatInfo = System.ValueTuple<System.Runtime.InteropServices.OSPlatform, System.Runtime.InteropServices.Architecture>;
using System.Reflection;
using System.Runtime.Serialization;

namespace SolcNet.Legacy
{
    public static class LibPath
    {
        const string LIB_NAME = "solc";

        static readonly Dictionary<PlatInfo, (string LibPrefix, string Extension)> PlatformPaths = new Dictionary<PlatInfo, (string, string)>
        {
            [(Windows, X64)] = ("", ".dll"),
            [(Linux, X64)] = ("lib", ".so"),
            [(OSX, X64)] = ("lib", ".dylib"),
        };

        static readonly OSPlatform[] SupportedPlatforms = { Windows, OSX, Linux };
        static string SupportedPlatformDescriptions() => string.Join("\n", PlatformPaths.Keys.Select(GetPlatformDesc));

        static string GetPlatformDesc((OSPlatform OS, Architecture Arch) info) => $"{info.OS}; {info.Arch}";

        static readonly OSPlatform CurrentOSPlatform = SupportedPlatforms.FirstOrDefault(IsOSPlatform);
        static readonly PlatInfo CurrentPlatformInfo = (CurrentOSPlatform, ProcessArchitecture);
        static readonly Lazy<string> CurrentPlatformDesc = new Lazy<string>(() => GetPlatformDesc((CurrentOSPlatform, ProcessArchitecture)));

        static readonly Dictionary<(PlatInfo, SolcVersion), string> Cache = new Dictionary<(PlatInfo, SolcVersion), string>();

        /// <summary>
        /// Finds the full file path of the native solc library for the current operating system platform and architecture.
        /// </summary>
        public static string GetLibPath(SolcVersion version)
        {
            if (Cache.TryGetValue((CurrentPlatformInfo, version), out string result))
            {
                return result;
            }
            if (!PlatformPaths.TryGetValue(CurrentPlatformInfo, out (string LibPrefix, string Extension) platform))
            {
                throw new Exception(string.Join("\n", $"Unsupported platform: {CurrentPlatformDesc.Value}", "Must be one of:", SupportedPlatformDescriptions()));
            }

            var type = version.GetType();
            var versionString = type.GetField(Enum.GetName(type, version)).GetCustomAttribute<EnumMemberAttribute>().Value;

            var searchedPaths = new HashSet<string>();

            foreach (var containerDir in GetSearchLocations())
            {
                foreach (var libPath in SearchContainerPaths(containerDir, versionString, LIB_NAME, platform))
                {
                    if (!searchedPaths.Contains(libPath) && File.Exists(libPath))
                    {
                        Cache[(CurrentPlatformInfo, version)] = libPath;
                        return libPath;
                    }
                    searchedPaths.Add(libPath);
                }
            }

            throw new Exception($"Platform can be supported but '{LIB_NAME}' lib not found for {CurrentPlatformDesc.Value} at: {Environment.NewLine}{string.Join(Environment.NewLine, searchedPaths)}");

        }

        static IEnumerable<string> GetSearchLocations()
        {
            // If the this lib is being executed from its nuget package directory then the native
            // files should be found up a couple directories.
            yield return Path.GetFullPath(
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "../../content"));

            yield return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            yield return Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
            yield return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        static IEnumerable<string> SearchContainerPaths(string containerDir, string version, string library, (string LibPrefix, string Extension) platform)
        {
            yield return Path.Combine(containerDir, version, platform.LibPrefix + library + platform.Extension);
            yield return Path.Combine(containerDir, "Native", version, platform.LibPrefix + library + platform.Extension);
        }


    }
}
