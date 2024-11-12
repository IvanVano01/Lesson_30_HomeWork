using System;

public class GameDefeatNumberEnymiesInScence : IConditionsDefeat, IDisposable
{
    public event Action ÑonditionDefeatfulfilled;

    private int _numberEnemiesInSceneForDefeat;
    private int _currentNumberEnemiesInScene;
   
    private SpawnerEnemy _spawner;
    


    public GameDefeatNumberEnymiesInScence(int numberEnemiesInSceneForDefeat, SpawnerEnemy spawner)
    {
        _numberEnemiesInSceneForDefeat = numberEnemiesInSceneForDefeat;
        _currentNumberEnemiesInScene = 0;
        
        _spawner = spawner;
        _spawner.SpawnedNewEnemy += OnSpawnedNewEnemy;
        _spawner.KilledEnemy += OnKilledEnemy;                       
    }

    public void ProcessingDefeatCondition()
    {
        if (_currentNumberEnemiesInScene >= _numberEnemiesInSceneForDefeat)
            ÑonditionDefeatfulfilled?.Invoke();
    }

    public void Dispose()
    {
        _spawner.SpawnedNewEnemy -= OnSpawnedNewEnemy;
    }

    private void OnKilledEnemy()
    {
        _currentNumberEnemiesInScene--;
        ProcessingDefeatCondition();
    }

    private void OnSpawnedNewEnemy()
    {
        _currentNumberEnemiesInScene++;
        ProcessingDefeatCondition();
    }

}
