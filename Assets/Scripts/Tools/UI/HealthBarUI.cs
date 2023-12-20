using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBarUI : MonoBehaviour
{
    Image healthSlider;

    void Awake() 
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameManager.Instance.UpdateHealthOnHurt += UpdateHealthBar;
        healthSlider = transform.GetChild(0).GetComponent<Image>();
    }

    void Start()
    {
        GameManager.Instance.AddWaitGameObjectAndSetActiveFalse(this.gameObject);
    }

    void OnDestroy()
    {
        GameManager.Instance.UpdateHealthOnHurt -= UpdateHealthBar;
        healthSlider = null;
    }

    void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        float silderPercentage = (float)currentHealth / maxHealth;
        healthSlider.fillAmount = silderPercentage;
    }
}
