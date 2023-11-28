using UnityEngine;
using UnityEngine.UI;

public class health_bar : MonoBehaviour
{
    [SerializeField] private health playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start()
    {
        // Postavlja pocetnu vrednosti helt bara na samom pocetku
        totalHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 10;

    }

}
