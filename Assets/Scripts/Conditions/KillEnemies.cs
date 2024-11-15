using System;

public class KillEnemies : IConditions, IDisposable
{
    public event Action Ņonditionfulfilled;

    private int _numberKilledEnemiesForVictory;
    private int _currentKilledEnemies;

    private SpawnerEnemy _spawnerEnemy;
    private Player _player;
    private KillsEnemyView _killsEnemyView;

    public KillEnemies(int numberKilledEnemiesForVictory, SpawnerEnemy spawnerEnemy, Player player, KillsEnemyView killsEnemyView)
    {
        _player = player;
        _spawnerEnemy = spawnerEnemy;
        _numberKilledEnemiesForVictory = numberKilledEnemiesForVictory;
        _killsEnemyView = killsEnemyView;
        _currentKilledEnemies = 0;
        _killsEnemyView.Show();
        _spawnerEnemy.KilledEnemy += OnKilledEnemy;
    }

    public void OnDisable()
    {
        _player.SetShoot(false);
    }

    public void OnEnable()
    {
        _player.ActivateHealthBar();
        _player.SetShoot(true);
    }

    public void Update()
    {
        if (_currentKilledEnemies >= _numberKilledEnemiesForVictory)
        {
            _player.SetShoot(false);
            Ņonditionfulfilled?.Invoke();
        }
    }

    public void Dispose()
    {
        _spawnerEnemy.KilledEnemy -= OnKilledEnemy;
    }

    private void OnKilledEnemy()
    {
        _currentKilledEnemies++;
        Update();
    }
}
