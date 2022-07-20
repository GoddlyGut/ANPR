using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using Rage;
using ANPR.Common;

namespace ANPR.Common
{
    public class CheckConflicts
    {
        internal static bool PreloadedChecks()
        {
            if (isRPHVersionHighEnough() && isLSPDFRersionHighEnough() && isStopThePedRunning() && DoesCommonDLLExist())
            {
                return true;
            }

            return false;

        }

        private static bool DoesCommonDLLExist()
        {
            return CheckAssemblyVersion("GoddlyGut-Common.dll", "GoddlyGutCommon DLL", RequiredFileInfo.RequiredGoddlyGutCommonVersion);
        }

        private static bool isRPHVersionHighEnough()
        {
            return CheckAssemblyVersion("RAGEPluginHook.exe", "RAGE Plugin Hook", RequiredFileInfo.RequiredRPHVersion);
        }

        private static bool isLSPDFRersionHighEnough()
        {
            return CheckAssemblyVersion(@"Plugins\LSPD First Response.dll", "LSPDFR", RequiredFileInfo.RequiredLSPDFRVersion);
        }

        private static bool isStopThePedRunning()
        {
            return GoddlyGut_Common.DependencyChecker.IsLSPDFRPluginRunnning("StopThePed");
        }

        private static bool CheckAssemblyVersion(string FilePath, string FileName, string RequiredVersion)
        {
            bool isValidFile = true;
            Dictionary<bool, string> ErrorToReturn = new Dictionary<bool, string>();

            try
            {
                if (System.IO.File.Exists(FilePath))
                {
                    Version mRequiredVersion = Version.Parse(RequiredVersion); //Converts to a readable version
                    Version mInstalledVersion = Version.Parse(FileVersionInfo.GetVersionInfo(FilePath).FileVersion); // Converts to a readable version
                    if (mRequiredVersion.CompareTo(mInstalledVersion) > 0)
                    {
                        GoddlyGut_Common.Common.NotificationHandler.DisplayNotificationComplex("Compatibility Check", "", string.Format("~r~ERROR: ~w~v{0} of file {1} is required! ~r~v{2} found.", mRequiredVersion, FileName, mInstalledVersion));
                        GoddlyGut_Common.Common.LoggingHandler.LogTrivial(string.Format("~r~ERROR: ~w~v{0} of file {1} is required! ~r~v{2}~w~ found.", mRequiredVersion, FileName, mInstalledVersion));
                        isValidFile = false;
                    }
                }
                else
                {
                    GoddlyGut_Common.Common.NotificationHandler.DisplayNotificationComplex("Compatibility Check", "", string.Format("Error while checking for file: ~r~{0}. File is missing: {1}", FileName));
                    GoddlyGut_Common.Common.LoggingHandler.LogTrivial(string.Format("Error while checking for file: ~r~{0}~w~. File is missing: {1}", FileName));
                    isValidFile = false;
                }
            }
            catch (Exception ex)
            {
                GoddlyGut_Common.Common.NotificationHandler.DisplayNotificationComplex("Compatibility Check", "", string.Format("Error while checking for ~r~{0}~w~: {1}", FileName, ex.ToString()));
                GoddlyGut_Common.Common.LoggingHandler.LogTrivial(string.Format("Error while checking for {0}: {1}", FileName, ex.ToString()));
            }
            return isValidFile;
        }

    }
}

