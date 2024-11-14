using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberEnymiesInScence : IConditions
{
    public event Action Ñonditionfulfilled;

    private int _numberEnemiesInSceneForDefeat;
    private int _currentNumberEnemiesInScene;

    private SpawnerEnemy _spawner;

    public NumberEnymiesInScence(int numberEnemiesInSceneForDefeat, SpawnerEnemy spawner)
    {
        _numberEnemiesInSceneForDefeat = numberEnemiesInSceneForDefeat;
        _currentNumberEnemiesInScene = 0;

        _spawner = spawner;
        _spawner.SpawnedNewEnemy += OnSpawnedNewEnemy;
        _spawner.KilledEnemy += OnKilledEnemy;
    }


    public void OnDisable()
    {
        
    }

    public void OnEnable()
    {
        
    }

    public void Update()
    {
        Debug.Log($" òåêóùåå êîë-âî âðàãîâ {_currentNumberEnemiesInScene}");
        if (_currentNumberEnemiesInScene >= _numberEnemiesInSceneForDefeat)
            Ñonditionfulfilled?.Invoke();
    }

    public void Dispose()
    {
        _spawner.SpawnedNewEnemy -= OnSpawnedNewEnemy;
    }

    private void OnKilledEnemy()
    {
        _currentNumberEnemiesInScene--;
        Update();
    }

    private void OnSpawnedNewEnemy()
    {
        _currentNumberEnemiesInScene++;
        Update();
    }
}
