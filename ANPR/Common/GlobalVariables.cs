using System;
using System.Diagnostics;

namespace ANPR.Common
{
    internal class GlobalVariables
    {
        internal const string dllPath = "@Plugins/LSPDFR/ANPR.dll";

        internal static FileVersionInfo FileVersion = FileVersionInfo.GetVersionInfo(dllPath);

        internal static string PluginName = FileVersion.ProductName.ToString();
        internal static string PluginVersion = FileVersion.FileVersion;

        internal static bool isPlayerOnDuty = false;
        internal static bool isANPRActive = false;
    }
}

