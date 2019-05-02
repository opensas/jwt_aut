using System;
using System.IO;

using System.Reflection;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Runtime.InteropServices;

namespace WebApi.Helpers
{
    public static class Info {

        // see: http://www.hishambinateya.com/goodbye-platform-abstractions

        public static class Platform {

            public static string CSharpVersion() =>
                typeof(string).Assembly.ImageRuntimeVersion;

            public static string Runtime() =>
                Assembly.GetEntryAssembly().GetCustomAttribute<TargetFrameworkAttribute>().FrameworkName;

            public static string Executable() =>
                Process.GetCurrentProcess().MainModule.FileName;

            public static string Framework() {
                return $".NET Core {NetCoreVersion()} (Framework {FrameworkVersion()})";
            }

            public static string FrameworkVersion() =>
                RuntimeInformation.FrameworkDescription.Replace(".NET Core ", "");

            public static string NetCoreVersion() {
                var assembly = typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly;
                var assemblyPath = assembly.CodeBase.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
                int netCoreAppIndex = Array.IndexOf(assemblyPath, "Microsoft.NETCore.App");
                if (netCoreAppIndex > 0 && netCoreAppIndex < assemblyPath.Length - 2)
                    return assemblyPath[netCoreAppIndex + 1];
                return "?";
            }

        }

        public static class Application {

            public static string Name() =>
                Assembly.GetEntryAssembly().GetName().Name.ToString();

            public static string Version() =>
                Assembly.GetEntryAssembly().GetName().Version.ToString();

            public static string BasePath() =>
                AppContext.BaseDirectory;

            public static string CurrentDirectory() =>
                Directory.GetCurrentDirectory();

        }

        public static class OS {

            public static string Description() =>
                RuntimeInformation.OSDescription;

            public static string Architecture() =>
                RuntimeInformation.OSArchitecture.ToString();

            public static string ProcessArchitecture() =>
                RuntimeInformation.ProcessArchitecture.ToString();

            public static string Server() =>
                Environment.GetEnvironmentVariable("COMPUTERNAME") ??
                    Environment.GetEnvironmentVariable("HOSTNAME") ?? "";

        }

    }

}