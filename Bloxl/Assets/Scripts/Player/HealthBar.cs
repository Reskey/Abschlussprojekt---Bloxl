using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Skripts.Player
{
    internal class HealthBar : MonoBehaviour
    {
        [SerializeField] Slider slider;
        [SerializeField] Gradient gradient;
        [SerializeField] Image fill;

        public void SetMaxHealth(int health)
        {
            slider.maxValue = health;
            slider.value = health;

            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

        public void SetHealth(float health)
        {
            slider.value = health;

            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

        public float GetHealth()
        {
            return slider.value;
        }
    }

}