using System;
using Rage;
using ANPR.Common;

namespace ANPR.Common
{
    public class CheckConflicts
    {
        internal static bool PreloadedChecks()
        {
            if (isRPHVersionHighEnough() && isLSPDFRersionHighEnough() && isStopThePedRunning())
            {
                return true;
            }

            return false;

        }


        private static bool isRPHVersionHighEnough()
        {
            return GoddlyGut_Common.DependencyChecker.CheckAssemblyVersion("RAGEPluginHook.exe", "RAGE Plugin Hook", RequiredFileInfo.RequiredRPHVersion);
        }

        private static bool isLSPDFRersionHighEnough()
        {
            return GoddlyGut_Common.DependencyChecker.CheckAssemblyVersion(@"Plugins\LSPD First Response.dll", "LSPDFR", RequiredFileInfo.RequiredLSPDFRVersion);
        }

        private static bool isStopThePedRunning()
        {
            return GoddlyGut_Common.DependencyChecker.IsLSPDFRPluginRunnning("StopThePed");
        }
    }
}

