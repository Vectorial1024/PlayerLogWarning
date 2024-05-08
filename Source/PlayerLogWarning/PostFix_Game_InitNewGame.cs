using HarmonyLib;
using PlayerLogWarning;
using Verse;

namespace EBF.Patches
{
    [HarmonyPatch(typeof(Game))]
    [HarmonyPatch(nameof(Game.InitNewGame))]
    public class PostFix_Game_InitNewGame
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
