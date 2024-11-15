using System;

public class PlayerDied : IConditions
{
    public event Action Ņonditionfulfilled;

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
        Ņonditionfulfilled?.Invoke();
    }
}
