using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient colors;
    [SerializeField] private Image fill;
    private float maxHealth;

    public void SetMaxHealth(float health)
    {
        maxHealth = slider.maxValue = health;
        slider.value = maxHealth;

        fill.color = colors.Evaluate(1);
    }

    public void SetHealth(float health)
    {
        slider.value = health;

        fill.color = colors.Evaluate(health / maxHealth);
    }
}
  