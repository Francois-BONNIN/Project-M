using UnityEngine;
using UnityEngine.UI; //biblihotheque pour "interface"

public class HealthBar : MonoBehaviour
{
    public Slider slider; //par rapport au component slider

    public void SetMaxHealth(int health) //applique la vie max et donc réinitialise ses point de vie
    {
        slider.maxValue = health; //parametre dans slider (vie max ici 100pv)
        slider.value = health; //parametre dans slider
    }

    public void SetHealth(int health) //indique à la barre de vie les pv à afficher
    {
        slider.value = health;
    }
}
