using System.Collections.Generic;
using UnityEngine;

namespace WaterWarning
{
    public class WaterWarning : MonoBehaviour
    {
        public BoatDamage boatDamage;
        public List<ShipItemLight> lanterns;
        public static int[] lanternIndices = { 112 }; // prefabIndexes of blink-appropriate lantern items
        public static float interval = 1f; // blinking interval in seconds
        public static float waterThreshold = 0.7f; // Water level threshold to activate lanterns
        bool on = false;
        private float timer = 0f;

        public void Awake()
        {
            lanterns = new List<ShipItemLight>();
            boatDamage = GetComponent<BoatDamage>();
        }

        public void Update()
        {
            if (lanterns.Count > 0 && boatDamage != null && boatDamage.waterLevel > waterThreshold)
            {
                timer += Time.deltaTime;
                if (timer > interval)
                {
                    timer = 0f;
                    foreach (var lantern in lanterns)
                    {
                        lantern.OnAltActivate();
                    }
                    on = !on;
                }
            }
            else if (on)
            {
                foreach (var lantern in lanterns)
                {
                    lantern.OnAltActivate();
                }
                on = false;
            }
        }
    }
}
