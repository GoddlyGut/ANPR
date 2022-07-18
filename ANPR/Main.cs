using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ANPR.Common;
using LSPD_First_Response.Mod.API;
using Rage;


namespace ANPR
{
    public class Main : Plugin
    {
        public override void Initialize()
        {
            Functions.OnOnDutyStateChanged += Functions_OnOnDutyStateChanged;
            GoddlyGut_Common.Common.LoggingHandler.LogTrivial(string.Format("{0} has been loaded. Version installed: {1}", GlobalVariables.PluginName, GlobalVariables.PluginVersion));
            GoddlyGut_Common.Common.LoggingHandler.LogTrivial(string.Format("Go on duty to fully load {0}", GlobalVariables.PluginName));
        }

        public override void Finally()
        {
            GlobalVariables.isPlayerOnDuty = false;
        }

        internal static void Functions_OnOnDutyStateChanged(bool onDuty)
        {
            if (onDuty)
            {
                GlobalVariables.isPlayerOnDuty = true;
                if (CheckConflicts.PreloadedChecks())
                {
                    LoadPlugin(onDuty);
                }
            }
        }

        private static void LoadPlugin(bool onDuty)
        {
            if (onDuty)
            {

                GameFiber.StartNew(delegate
                {
                    GoddlyGut_Common.Common.NotificationHandler.DisplayNotificationComplex(string.Format("{0} ~y~v{1}", GlobalVariables.PluginName, GlobalVariables.PluginVersion), string.Format("By: ~b~GoddlyGut"), string.Format("{0} has been loaded successfully!", GlobalVariables.PluginName));
                });
            }
        }


    }
}
