using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{    public Slider healthSlider; 
    public Gradient healthGradient;
    public Image fillImage; 
    
    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health; 
        healthSlider.value = health; 
        fillImage.color = healthGradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health; 
        fillImage.color = healthGradient.Evaluate(healthSlider.normalizedValue); 
    }
      
    
}

