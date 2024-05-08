using HugsLib;
using Verse;

namespace PlayerLogWarning
{
    internal class PlayerLogWarningMain : ModBase
    {
        public static string MODSHORTID => "V1024-PLW";

        public override string LogIdentifier => MODSHORTID;

        /// <summary>
        /// Already includes a space character.
        /// </summary>
        public static string MODPREFIX => "[" + MODSHORTID + "] ";

        public static void LogError(string message)
        {
            Log.Error(MODPREFIX + " " + message);
        }

        public static void LogWarning(string message)
        {
            Log.Warning(MODPREFIX + " " + message);
        }
    }
}
