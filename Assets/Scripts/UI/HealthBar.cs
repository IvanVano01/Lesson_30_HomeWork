using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    private Slider _slider;
    private Health _health;

    public void Initialize(Health health)
    {
        _slider = GetComponent<Slider>();
        _health = health;
        _health.ChangedHealth += OnChangedHealth;
        _slider.value = 1;
    }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);

    private void OnDestroy()
    {
        _health.ChangedHealth -= OnChangedHealth;
    }

    private void OnChangedHealth(int currentHealth)
    {
        float healthInPrecantage = (float)currentHealth / _health.MaxHealth;

        _slider.value = healthInPrecantage;        
    }
}
