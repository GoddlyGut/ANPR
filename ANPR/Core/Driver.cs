using System;
using GoddlyGut_Common;
using ANPR.Common;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Engine.Scripting.Entities;
using Rage;
using System.Windows.Forms;


namespace ANPR.Core
{
    public class Driver
    {

        internal void Launch()  
        {
            while (GlobalVariables.isPlayerOnDuty)
            {
                if (GlobalVariables.isANPRActive)
                {
                    
                }
                GameFiber.Yield();
            }
        }

        internal void ListenForToggle()
        {
            while (GlobalVariables.isPlayerOnDuty)
            {
                if (Game.IsKeyDown(Keys.F2))
                {
                    ToggleFunction();
                }

                GameFiber.Yield();
            }
        }

        private void ToggleFunction()
        {
            ResponseTypes responseToLog = ResponseTypes.Cleared;

            if (GlobalVariables.PlayerVehicle.Exists())
            {
                if (!GlobalVariables.PlayerPed.IsOnFoot)
                {
                    if (GlobalVariables.PlayerVehicle.IsPoliceVehicle)
                    {
                        GlobalVariables.isANPRActive = !GlobalVariables.isANPRActive;

                        if (GlobalVariables.isANPRActive)
                        {
                            AudioHandler.PlaySound(AudioHandler.ESounds.ThermalVisionOn);
                            GoddlyGut_Common.Common.NotificationHandler.DisplayNotificationSimple("~b~ALPR~w~ is ~g~ACTIVATED");
                        }
                        else
                        {
                            AudioHandler.PlaySound(AudioHandler.ESounds.ThermalVisionOff);
                            GoddlyGut_Common.Common.NotificationHandler.DisplayNotificationSimple("~b~ALPR~w~ is ~r~DEACTIVATED");
                        }
                    }
                    else { responseToLog = ResponseTypes.VehicleNotPoliceVehicle; }

                }
                else { responseToLog = ResponseTypes.PlayerOnFoot; }
            }
            else { responseToLog = ResponseTypes.NoVehicleFound; }


            switch (responseToLog)
            {
                case ResponseTypes.Cleared:
                    GoddlyGut_Common.Common.LoggingHandler.LogTrivial("Player successfully toggled ANPR system");
                    break;
                case ResponseTypes.NoVehicleFound:
                    GoddlyGut_Common.Common.NotificationHandler.DisplayHelpNotification("To toggle ~b~ANPR~w~, you have to be inside a ~y~police vehicle~w~.");
                    GoddlyGut_Common.Common.LoggingHandler.LogTrivial("Player attempted to activate ANPR without a vehicle");
                    break;
                case ResponseTypes.PlayerOnFoot:
                    GoddlyGut_Common.Common.NotificationHandler.DisplayHelpNotification("To toggle ~b~ANPR~w~, you have to be inside a ~y~police vehicle~w~.");
                    GoddlyGut_Common.Common.LoggingHandler.LogTrivial("Player attempted to activate ANPR while on foot");
                    break;
                case ResponseTypes.VehicleNotPoliceVehicle:
                    GoddlyGut_Common.Common.LoggingHandler.LogTrivial("Player attempted to activate ANPR while in a non-police vehicle");
                    break;
            }
        }

        internal enum ResponseTypes
        {
            Cleared,
            NoVehicleFound,
            VehicleNotPoliceVehicle,
            PlayerOnFoot
        }
    }
}

