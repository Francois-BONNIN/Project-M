using UnityEngine.UI;
using UnityEngine;

public class ShieldBar : MonoBehaviour
{
    public Slider slider; 
    public void SetMaxShield(int shield) 
    {
        slider.maxValue = shield; 
        slider.value = shield; 
    }

    public void SetShield(int shield) 
    {
        slider.value = shield;
    }
}
