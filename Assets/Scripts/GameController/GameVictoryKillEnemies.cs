using System;

public class GameVictoryKillEnemies : IConditionsVictory, IDisposable
{
    public event Action ÑonditionVictoryfulfilled;

    private int _numberKilledEnemiesForVictory;
    private int _currentKilledEnemies;
    
    private SpawnerEnemy _spawnerEnemy;
    private Player _player;
    private KillsEnemyView _killsEnemyView;


    public GameVictoryKillEnemies(int numberKilledEnemiesForVictory, SpawnerEnemy spawnerEnemy, Player player, KillsEnemyView killsEnemyView)
    { 
        _player = player;
        _player.SetShoot(true);
        _spawnerEnemy = spawnerEnemy;
        _numberKilledEnemiesForVictory = numberKilledEnemiesForVictory;
        _killsEnemyView = killsEnemyView;
        _currentKilledEnemies = 0;
        _killsEnemyView.Show();

        _spawnerEnemy.KilledEnemy += OnKilledEnemy;        
    }

    public void ProcessingWinCondition()
    {
        if (_currentKilledEnemies >= _numberKilledEnemiesForVictory)
            ÑonditionVictoryfulfilled?.Invoke();            
    }

    public void Dispose()
    {
        _spawnerEnemy.KilledEnemy -= OnKilledEnemy;
    }

    private void OnKilledEnemy()
    {
        _currentKilledEnemies++;
        ProcessingWinCondition();
    }       
}
