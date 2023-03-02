using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        [SerializeField] TMP_Text text;

        public void SetMaxHealth(int health)
        {
            slider.maxValue = health;
            slider.value = health;
            text.text = health.ToString();

            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

        public void SetHealth(float health)
        {
            slider.value = health;
            text.text = health.ToString();

            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

        public float GetHealth()
        {
            return slider.value;
        }
    }

}
