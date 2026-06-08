using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Slider healthSlider; // Reference to the UI Slider component
    public Gradient healthGradient;
    public Image fillImage; // Reference to the Image component that fills the slider
    
    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health; // Set the maximum value of the slider to the player's max health
        healthSlider.value = health; // Initialize the slider value to the max health
        fillImage.color = healthGradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health; // Update the slider value to reflect current health
        fillImage.color = healthGradient.Evaluate(healthSlider.normalizedValue); // Change the color based on the current health
    }
      
    
}

