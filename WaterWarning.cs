using System.Collections.Generic;
using UnityEngine;

namespace WaterWarning
{
    public class WaterWarning : MonoBehaviour
    {
        public BoatDamage boatDamage;
        public Rigidbody boatRigidbody;
        public Dictionary<ShipItemLight, HangableItem> lanterns;
        public static int[] lanternIndices = { 112 }; // prefabIndexes of blink-appropriate lantern items
        public static float interval = 1f; // blinking interval in seconds
        public static float waterThreshold = 0.7f; // Water level threshold to activate lanterns
        bool on = false;
        private float timer = 0f;

        public void Awake()
        {
            lanterns = new Dictionary<ShipItemLight, HangableItem>();
            boatDamage = GetComponent<BoatDamage>();
            boatRigidbody = GetComponent<Rigidbody>();
        }

        public void Update()
        {
            if (boatRigidbody.isKinematic || !GameState.playing || (Plugin.singleBoat.Value && GameState.lastBoat != this.gameObject))
            {
                return;
            }
            if (lanterns.Count > 0 && boatDamage != null && boatDamage.waterLevel > Plugin.waterThreshold.Value)
            {
                timer += Time.deltaTime;
                if (timer > interval)
                {
                    timer = 0f;
                    ToggleLanterns();
                    on = !on;
                }
            }
            else if (on)
            {
                ToggleLanterns();
                on = false;
            }
        }
        private void ToggleLanterns()
        {
            foreach (var lantern in lanterns)
            {
                if (lantern.Key.held || (Plugin.hangingOnly.Value && !lantern.Value.IsHanging()))
                {
                    continue;
                }
                lantern.Key.OnAltActivate();
            }
        }
    }
}
