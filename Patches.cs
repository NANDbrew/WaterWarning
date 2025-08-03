using HarmonyLib;
using System.Linq;

namespace WaterWarning
{
    internal class Patches
    {
        [HarmonyPatch(typeof(BoatDamage), "Start")]
        public static class BoatDamageStartPatch
        {
            public static void Postfix(BoatDamage __instance)
            {
                __instance.gameObject.AddComponent<WaterWarning>();
            }
        }
        [HarmonyPatch(typeof(ShipItem), "EnterBoat")]
        public static class LanternEmbarkpatch
        {
            public static void Postfix(ShipItem __instance, bool __runOriginal)
            {
                if (__runOriginal && __instance is ShipItemLight light && WaterWarning.lanternIndices.Contains(__instance.GetPrefabIndex()))
                {
                    __instance.currentActualBoat.parent.GetComponent<WaterWarning>()?.lanterns.Add(light);
                }
            }
        }
        [HarmonyPatch(typeof(ShipItem), "ExitBoat")]
        public static class LanternDisembarkpatch
        {
            public static void Prefix(ShipItem __instance, bool __runOriginal)
            {
                if (__runOriginal && __instance is ShipItemLight light)
                {
                    __instance.currentActualBoat.parent.GetComponent<WaterWarning>()?.lanterns.Remove(light);
                }
            }
        }
    }
}
