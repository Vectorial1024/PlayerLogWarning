using HarmonyLib;
using System;
using Verse;

namespace PlayerLogWarning
{
    public class PlayerLogWarningMod : Mod
    {
        public static string MODSHORTID => "V1024-PLW";

        public PlayerLogWarningMod(ModContentPack content) : base(content)
        {
            // since we no longer depend on HugsLib, we have to apply the harmony patches ourselves
            LogInfo("Player Log Warning, starting up. Hopefully the patches work.");
            // also print RimWorld (assembly) version
            // supposedly the log will also print RimWorld version (more accurate), but sometimes I just miss it.
            Version rimworldVersion = typeof(Mod).Assembly.GetName().Version;
            LogInfo($"Detecting your RimWorld assembly version as {rimworldVersion}");
            Harmony harmony = new Harmony("rimworld." + content.PackageId);
            harmony.PatchAll();
        }

        /// <summary>
        /// Already includes a space character.
        /// </summary>
        public static string MODPREFIX => "[" + MODSHORTID + "]";

        public static void LogError(string message)
        {
            Log.Error(MODPREFIX + " " + message);
        }

        public static void LogWarning(string message)
        {
            Log.Warning(MODPREFIX + " " + message);
        }

        public static void LogInfo(string message)
        {
            Log.Message(MODPREFIX + " " + message);
        }
    }
}
