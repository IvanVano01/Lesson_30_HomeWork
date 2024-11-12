using System;
using UnityEngine;

public class Health 
{
    public event Action<int> ChangedHealth;

    public Health(int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;
    }

    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    public virtual void Reduce(int value)
    {
        if (value < 0)
        {
            Debug.LogError(" Внимание! Значение здоровья < 0");
            return;
        }

        CurrentHealth -= value;

        CurrentHealth = Mathf.Clamp(CurrentHealth,0, MaxHealth);

        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }
        ChangedHealth?.Invoke(CurrentHealth);       
    }
}
