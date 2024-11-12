using System;

public class GameDefeatPlayerDied : IConditionsDefeat, IDisposable
{    
    public event Action �onditionDefeatfulfilled;

    private Player _player; 

    public GameDefeatPlayerDied(Player player)
    {
        _player = player;       

        _player.DeidPlayer += OnDeidPlayer;

        _player.ActivateHealthBar();        
    }

    private void OnDeidPlayer()
    {
        ProcessingDefeatCondition();
    }

    public void ProcessingDefeatCondition()
    {        
        �onditionDefeatfulfilled?.Invoke();        
    }

    public void Dispose()
    {
        _player.DeidPlayer -= OnDeidPlayer;
    }
}
