using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBarUI : MonoBehaviour
{
    Image healthSlider;

    GameManager gameManager;

    void Awake() 
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.UpdateHealthOnHurt += UpdateHealthBar;

        healthSlider = transform.GetChild(0).GetComponent<Image>();
    }

    void OnDestroy()
    {
        gameManager.UpdateHealthOnHurt -= UpdateHealthBar;
        gameManager = null;
        healthSlider = null;
    }

    void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        float silderPercentage = (float)currentHealth / maxHealth;
        healthSlider.fillAmount = silderPercentage;
    }
}
