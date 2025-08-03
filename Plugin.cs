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
        internal ConfigEntry<float> waterThreshold;


        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PLUGIN_ID);

            waterThreshold = Config.Bind("Settings", "Water Threshold", 0.75f, new ConfigDescription("Percentage to warn at. 100 is where the boat sinks", new AcceptableValueRange<float>(0f, 1f), new ConfigurationManagerAttributes { ShowRangeAsPercent = true }));

            waterThreshold.SettingChanged += (sender, args) => UpdateWaterThreshold();
            UpdateWaterThreshold();
        }

        private void UpdateWaterThreshold()
        {
            WaterWarning.waterThreshold = waterThreshold.Value;
        }
    }
}
