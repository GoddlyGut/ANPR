using System;
using System.Diagnostics;
using Rage;

namespace ANPR.Common
{
    internal class GlobalVariables
    {
        internal const string dllPath = "@Plugins/LSPDFR/ANPR.dll";

        internal static FileVersionInfo FileVersion = FileVersionInfo.GetVersionInfo(dllPath);

        internal static string PluginName = FileVersion.ProductName.ToString();
        internal static string PluginVersion = FileVersion.FileVersion;

        internal static bool isPlayerOnDuty { get; set; } = false;
        internal static bool isANPRActive = false;

        internal static Ped PlayerPed { get { return Game.LocalPlayer.Character; } }
        internal static Vehicle PlayerVehicle { get { return GlobalVariables.PlayerPed.CurrentVehicle; } }
    }
}

