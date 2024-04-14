using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider HealthBat;
    // Start is called before the first frame update
    public void SetMaxHealth(int health)
    {
        HealthBat.maxValue = health;
        HealthBat.value = health;
    }

    public void SetHealth(int health)
    {
        HealthBat.value = health;
    }
}
