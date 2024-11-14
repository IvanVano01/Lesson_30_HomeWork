using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDied : IConditions
{
    public event Action Ñonditionfulfilled;

    private Player _player;

    public PlayerDied(Player player)
    {
        _player = player;

        _player.DeidPlayer += OnDeidPlayer;        
    }

    public void OnEnable()
    {
        _player.ActivateHealthBar();
    }

    public void OnDisable()
    {
        
    }

    public void Update()
    {
        
    }

    public void Dispose()
    {
        _player.DeidPlayer -= OnDeidPlayer;
    }
    
    private void OnDeidPlayer()
    {
        Ñonditionfulfilled?.Invoke();
    }
}
