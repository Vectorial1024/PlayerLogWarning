using HarmonyLib;
using PlayerLogWarning;
using Verse;

namespace EBF.Patches
{
    [HarmonyPatch(typeof(Game))]
    [HarmonyPatch(nameof(Game.LoadGame))]
    public class PostFix_Game_LoadGame
    {
        [HarmonyPostfix]
        public static void PostFix()
        {
            LongEventHandler.ExecuteWhenFinished(delegate
            {
                LogFileChecker.CheckOnce();
            });
        }
    }
}
