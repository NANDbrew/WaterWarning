using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Reflection;

namespace WaterWarning
{
    [BepInPlugin(PLUGIN_ID, PLUGIN_NAME, PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PLUGIN_ID = "com.nandbrew.WaterWarning";
        public const string PLUGIN_NAME = "Water Warning";
        public const string PLUGIN_VERSION = "1.0.0";

        //--settings--
        internal static ConfigEntry<float> waterThreshold;
        internal static ConfigEntry<bool> singleBoat;
        internal static ConfigEntry<bool> hangingOnly;

        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PLUGIN_ID);

            singleBoat = Config.Bind("Settings", "Single Boat", false, new ConfigDescription("If enabled, only lanterns on your most recent boat will be toggled. If disabled, all nearby boats will be affected.", null, new ConfigurationManagerAttributes { IsAdvanced = false }));
            hangingOnly = Config.Bind("Settings", "Hanging Lanterns Only", true, new ConfigDescription("If enabled, only lanterns that are hanging from a hook are affected. Otherwise, all lanterns except those you're holding will be affected.", null, new ConfigurationManagerAttributes { IsAdvanced = false }));
            waterThreshold = Config.Bind("Settings", "Water Threshold", 0.75f, new ConfigDescription("Percentage to warn at. 100 is where the boat sinks", new AcceptableValueRange<float>(0f, 1f), new ConfigurationManagerAttributes { ShowRangeAsPercent = true }));

        }
    }
}
